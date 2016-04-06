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
        public bool IgnoreDefault { get; set; }

        public string[] ExtensionsIgnored = new string[]
        {
            "pdb", "config", "manifest"
        };

        public string[] ContainsIgnored = new string[]
        {
            "vshost"
        };

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

        private bool Ignore(FileInfo file)
        {
            if (IgnoreDefault)
            {
                if (ExtensionsIgnored.Contains(file.Extension))
                {
                    return true;
                }

                for (int j = 0; j < ContainsIgnored.Length; j++)
                {
                    string ign = ContainsIgnored[j];
                    if (file.Name.Contains(ign))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void RecursiveFirstUpdate(DirectoryInfo parent, UpdateInfo update, string root)
        {
            string lowerRoot = root.ToLower();

            FileInfo[] files = parent.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];

                if (Ignore(file))
                {
                    continue;
                }

                EntryInfo entry = MakeFileAddEntry(update, file, root, lowerRoot);
                update.Entries.Add(entry);
            }

            DirectoryInfo[] dirs = parent.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];

                if (dir.Name == ProjectInfo.NukeName)
                {
                    continue;
                }

                string rel = Path.Combine(root, dir.Name);
                EntryInfo entry = MakeDirAddEntry(update, dir, root, lowerRoot);
                update.Entries.Add(entry);

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
                    entry.RelativePathLower = entry.RelativePath.ToLower();

                    latest.Entries[i] = entry; // structs
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

            List<EntryInfo> lastContent = latest.Entries.Where(c => c.Type == EntryType.File && c.RelativePathLower == lowerRoot).ToList();

            FileInfo[] files = parent.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];

                if (Ignore(file))
                {
                    continue;
                }

                // search for file on latest version
                string lowerName = file.Name.ToLower();
                var search = lastContent.Where(c => c.NameLower == lowerName);
                if (search.Count() == 0)
                {
                    EntryInfo entry = MakeFileAddEntry(update, file, root, lowerRoot);
                    update.Entries.Add(entry);
                }
                else
                {
                    EntryInfo last = search.First();
                    lastContent.Remove(last);

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

            // deleted files
            HandleDeletedFiles(update, lastContent);

            DirectoryInfo[] dirs = parent.GetDirectories();
            lastContent = latest.Entries.Where(c => c.Type == EntryType.Directory && c.Name != parent.Name && c.RelativePathLower == root).ToList();

            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];

                if (dir.Name == ProjectInfo.NukeName)
                {
                    continue;
                }

                string rel = Path.Combine(root, dir.Name);
                string relLower = rel.ToLower();

                // search for direcotry on latest version
                string lowerName = dir.Name.ToLower();
                var search = lastContent.Where(c => c.NameLower == lowerName && c.RelativePathLower == root);

                if (search.Count() == 0)
                {
                    EntryInfo entry = MakeDirAddEntry(update, dir, root, lowerRoot);
                    update.Entries.Add(entry);
                }
                else
                {
                    // cant change a folders hash because it has no content
                    // just pass it forward so to say it still exists
                    EntryInfo last = search.First();
                    lastContent.Remove(last);

                    last.State = EntryState.Unchanged;
                    update.Entries.Add(last);
                }

                RecursiveMakeUpdate(dir, latest, update, rel);
            }

            // deleted files
            HandleDeletedFiles(update, lastContent);
        }

        private void HandleDeletedFiles(UpdateInfo update, List<EntryInfo> entries)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                EntryInfo entry = entries[i];
                entry.State = EntryState.Removed;
                entry.LastUpdate = update.Revision;
                entry.Hash = string.Empty;
                update.Entries.Add(entry);
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
            entry.Type = EntryType.File;
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
            entry.Type = EntryType.Directory;
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
