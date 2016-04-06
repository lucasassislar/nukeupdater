using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.Api
{
    public class ProjectInfo
    {
        public static readonly string JsonFormat = ".json";

        public static readonly string NukeName = ".nuke";
        public static readonly string VersionName = "Version";
        public static readonly string VersionsPath = "Versions";
        public static readonly string ProjectInfoFile = "ProjectInfo.json";

        public string Name { get; set; }
        public int Latest { get; private set; }

        [JsonIgnore]
        public bool Created { get; private set; }

        [JsonIgnore]
        public string Root { get; private set; }


        private ProjectInfo()
        {
        }
        public ProjectInfo(string root)
        {
            Initialize(root);
        }

        private string nukeDir;
        private string versionsDir;

        private void Initialize(string root)
        {
            if (root.Contains(NukeName))
            {
                // dipshit, you're not supposed to select a project folder
                // but we'll work with it
                int index = root.IndexOf(NukeName);
                root = root.Remove(index, root.Length - index);
            }
            Root = root;

            nukeDir = Path.Combine(root, NukeName);
            versionsDir = Path.Combine(nukeDir, VersionsPath);

            Created = Directory.Exists(nukeDir);

            if (Created)
            {
                // see latest
                FileInfo[] files = new DirectoryInfo(versionsDir).GetFiles();
                int[] filesNumbers = new int[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    filesNumbers[i] = int.Parse(Path.GetFileNameWithoutExtension(files[i].Name).Remove(0, VersionName.Length));
                }
                Latest = filesNumbers.Max();
            }
        }

        public UpdateInfo ReadUpdate(int revision)
        {
            string path = Path.Combine(versionsDir, VersionName + revision.ToString(CultureInfo.InvariantCulture) + JsonFormat);

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<UpdateInfo>(json);
        }

        public void Make()
        {
            Directory.CreateDirectory(nukeDir);
            DirectoryInfo dir = new DirectoryInfo(nukeDir);
            dir.Attributes = FileAttributes.Hidden;

            Directory.CreateDirectory(versionsDir);
        }

        public void SaveUpdate(UpdateInfo update)
        {
            string path = Path.Combine(versionsDir, VersionName + update.Revision.ToString(CultureInfo.InvariantCulture) + JsonFormat);
            File.WriteAllText(path, JsonConvert.SerializeObject(update));

            // make directory
            string updateDir = Path.Combine(versionsDir, VersionName + update.Revision.ToString(CultureInfo.InvariantCulture));
            Directory.CreateDirectory(updateDir);

            for (int i = 0; i < update.Entries.Count; i++)
            {
                EntryInfo entry = update.Entries[i];
                if (entry.Type == EntryType.File)
                {
                    if (entry.State == EntryState.Added ||
                        entry.State == EntryState.Updated)
                    {
                        string relPath = Path.Combine(entry.RelativePath, entry.Name);
                        string from = Path.Combine(Root, relPath);
                        string to = Path.Combine(updateDir, relPath);
                        string dir = Path.GetDirectoryName(to);
                        Directory.CreateDirectory(dir);

                        File.Copy(from, to);
                    }
                }
            }
        }

        public void Save()
        {
            string file = Path.Combine(nukeDir, ProjectInfoFile);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
