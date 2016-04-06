using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.Api
{
    public class UpdateInfoBuilder : IDisposable
    {
        private MD5CryptoServiceProvider md5;

        public UpdateInfoBuilder()
        {
            md5 = new MD5CryptoServiceProvider();
        }

        public UpdateInfo MakeFirstUpdate(string parentDirectory)
        {
            UpdateInfo info = new UpdateInfo();
            info.Entries = new List<EntryInfo>();
            info.Revision = 0;

            DirectoryInfo dir = new DirectoryInfo(parentDirectory);
            RecursiveFirstUpdate(dir, info, "");

            return info;
        }

        private void RecursiveFirstUpdate(DirectoryInfo parent, UpdateInfo update, string root)
        {
            string lowerRoot = root.ToLower();

            FileInfo[] files = parent.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];

                EntryInfo entry = MakeFileAddEntry(update, file, root, lowerRoot);
                update.Entries.Add(entry);
            }

            DirectoryInfo[] dirs = parent.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];

                string rel = Path.Combine(root, dir.Name);
                EntryInfo entry = MakeDirAddEntry(update, dir, rel, rel.ToLower());
                RecursiveFirstUpdate(dir, update, rel);
            }
        }

        public UpdateInfo MakeUpdate(UpdateInfo latest, string parentDirectory)
        {
            if (latest.Entries.Count > 0 && string.IsNullOrEmpty(latest.Entries[0].NameLower))
            {
                for (int i = 0; i < latest.Entries.Count; i++)
                {
                    EntryInfo entry = latest.Entries[i];

                    entry.NameLower = entry.Name.ToLower();
                    entry.RelativePathLower = entry.RelativePathLower.ToLower();
                }
            }

            UpdateInfo update = new UpdateInfo();
            update.Entries = new List<EntryInfo>();
            update.Revision = latest.Revision + 1;

            DirectoryInfo dir = new DirectoryInfo(parentDirectory);
            RecursiveMakeUpdate(dir, latest, update, "");

            return update;
        }

        private void RecursiveMakeUpdate(DirectoryInfo parent, UpdateInfo latest, UpdateInfo update, string root)
        {
            string lowerRoot = root.ToLower();

            FileInfo[] files = parent.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];

                // search for file on latest version
                string lowerName = file.Name.ToLower();
                var search = latest.Entries.Where(c => c.NameLower == lowerName && c.RelativePathLower == lowerRoot);

                if (search.Count() == 0)
                {
                    EntryInfo entry = MakeFileAddEntry(update, file, root, lowerRoot);
                    update.Entries.Add(entry);
                }
                else
                {
                    EntryInfo last = search.First();

                    string hash;
                    using (Stream s = file.OpenRead())
                    {
                        hash = GetMD5HashFromFile(s);
                    }

                    if (hash == last.Hash)
                    {
                        last.State = EntryState.Unchanged;
                        update.Entries.Add(last);
                        continue;
                    }

                    // Update
                    last.State = EntryState.Updated;
                    last.Hash = hash;
                    last.LastUpdate = update.Revision;
                    update.Entries.Add(last);
                }
            }

            DirectoryInfo[] dirs = parent.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];

                string rel = Path.Combine(root, dir.Name);

                EntryInfo entry = new EntryInfo();
                entry.LastUpdate = 0;
                entry.Name = dir.Name;
                entry.NameLower = dir.Name.ToLower();
                entry.RelativePath = rel;
                entry.State = EntryState.Added;
                entry.Hash = "DIR";

                RecursiveMakeUpdate(dir, latest, update, rel);
            }
        }


        private EntryInfo MakeFileAddEntry(UpdateInfo update, FileInfo file, string root, string lowerRoot)
        {
            EntryInfo entry = new EntryInfo();
            entry.LastUpdate = update.Revision;
            entry.Name = file.Name;
            entry.NameLower = file.Name.ToLower();
            entry.RelativePath = root;
            entry.RelativePathLower = lowerRoot;
            entry.State = EntryState.Added;
            using (Stream s = file.OpenRead())
            {
                entry.Hash = GetMD5HashFromFile(s);
            }
            return entry;
        }

        private EntryInfo MakeDirAddEntry(UpdateInfo update, DirectoryInfo dir, string root, string lowerRoot)
        {
            EntryInfo entry = new EntryInfo();
            entry.LastUpdate = update.Revision;
            entry.Name = dir.Name;
            entry.NameLower = dir.Name.ToLower();
            entry.RelativePath = root;
            entry.RelativePathLower = lowerRoot;
            entry.State = EntryState.Added;
            entry.Hash = "DIR";
            return entry;
        }

        private string GetMD5HashFromFile(Stream instream)
        {
            var buffer = md5.ComputeHash(instream);
            var sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(buffer[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public void Dispose()
        {
            md5.Dispose();
            md5 = null;
        }
    }
}
