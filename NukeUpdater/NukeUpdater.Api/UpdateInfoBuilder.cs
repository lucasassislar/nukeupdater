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

            DirectoryInfo dir = new DirectoryInfo(parentDirectory);
            RecursiveFirstUpdate(dir, info, "");

            return info;
        }

        private void RecursiveFirstUpdate(DirectoryInfo parent, UpdateInfo update, string root)
        {
            FileInfo[] files = parent.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];

                EntryInfo entry = new EntryInfo();
                entry.LastUpdate = 0;
                entry.Name = file.Name;
                entry.RelativePath = root;
                using (Stream s = file.OpenRead())
                {
                    entry.Hash = GetMD5HashFromFile(s);
                }

                update.Entries.Add(entry);
            }

            DirectoryInfo[] dirs = parent.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];
                RecursiveFirstUpdate(dir, update, Path.Combine(root, dir.Name));
            }
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
