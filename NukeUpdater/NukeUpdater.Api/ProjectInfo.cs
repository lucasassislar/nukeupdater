using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.Api
{
    public class ProjectInfo
    {
        public static string JsonFormat = ".json";

        public static string NukeName = "nuke";
        public static string VersionName = "Version";
        public static string VersionsPath = "Versions";
        public static string ProjectInfoFile = "ProjectInfo.json";
        public static string UpdatesName = "Updates";

        public string Name { get; set; }
        public int Latest { get; set; }
        public string ServerUrl { get; set; }
        public bool FinishedUpdate { get; set; }

        [JsonIgnore]
        public bool Created { get; private set; }

        [JsonIgnore]
        public string Root { get; private set; }

        private CultureInfo c;

        public ProjectInfo()
        {
        }

        private string nukeDir;
        private string versionsDir;
        private bool isClient;

        public static void MakeDefault(string name, string server, int latest, bool embed)
        {
            if (!server.EndsWith("/"))
            {
                server = server + '/';
            }

            ProjectInfo proj = new ProjectInfo();
            proj.Latest = latest;
            proj.ServerUrl = server;
            proj.Name = name;
            string loc = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            proj.InitializeClient(loc);
            if (!File.Exists(proj.Location))
            {
                proj.Save();
            }

#if EMBED
            if (embed)
            {
                string updatesFolder = Path.Combine(loc, UpdatesName);
                Directory.Delete(updatesFolder, true);

                string exePath = Path.Combine(updatesFolder, name + ".exe");
                Directory.CreateDirectory(updatesFolder);
                File.WriteAllBytes(exePath, NukeUpdater.Api.Properties.Resources.NukeUpdater_App);
            }
#endif
        }

        public void InitializeClient(string root)
        {
            isClient = true;
            c = CultureInfo.InvariantCulture;

            Root = root;
            nukeDir = root;
            Created = true;
        }

        public void InitializeServer(string root)
        {
            isClient = false;
            c = CultureInfo.InvariantCulture;

            //if (root.Contains(NukeName))
            //{
            //    // dipshit, you're not supposed to select a project folder
            //    // but we'll work with it
            //    int index = root.IndexOf(NukeName);
            //    root = root.Remove(index, root.Length - index);
            //}

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

        public async Task<ProjectInfo> GetProjectFromServer()
        {
            using (WebClient client = new WebClient())
            {
                string url = ServerUrl + "/" + ProjectInfoFile;
                string json = await client.DownloadStringTaskAsync(url);
                ProjectInfo info = JsonConvert.DeserializeObject<ProjectInfo>(json);
                return info;
            }
        }

        public async Task<UpdateInfo> GetLatestVersionFromServer(ProjectInfo serverVersion)
        {
            return await GetVersionFromServer(serverVersion.Latest);
        }
        public async Task<UpdateInfo> GetVersionFromServer(int version)
        {
            using (WebClient client = new WebClient())
            {
                string url = ServerUrl + "/" + VersionsPath + "/" + VersionName + version.ToString(c) + JsonFormat;
                string json = await client.DownloadStringTaskAsync(url);
                UpdateInfo info = JsonConvert.DeserializeObject<UpdateInfo>(json);
                return info;
            }
        }


        public UpdateInfo ReadUpdate(int revision)
        {
            string path = Path.Combine(versionsDir, VersionName + revision.ToString(CultureInfo.InvariantCulture) + JsonFormat);
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<UpdateInfo>(json);
        }

        public void DownloadUpdateFromServer(UpdateInfo update)
        {
            string updateDir = Path.Combine(Root, "Update");
            if (Directory.Exists(updateDir))
            {
                Directory.Delete(updateDir, true);
            }
            Directory.CreateDirectory(updateDir);

            string rootUrl = ServerUrl + VersionsPath + "/" + VersionName + update.Revision.ToString(c) + "/";

            for (int i = 0; i < update.Entries.Count; i++)
            {
                EntryInfo entry = update.Entries[i];

                if (entry.Type == EntryType.Directory)
                {
                    continue;
                }

                if (entry.State == EntryState.Added ||
                    entry.State == EntryState.Updated)
                {
                    string relPath = Path.Combine(entry.RelativePath, entry.Name);

                    // check the local file
                    string localPath = Path.Combine(Root, relPath);

                    if (File.Exists(localPath))
                    {
                        using (Stream inStream = File.OpenRead(localPath))
                        {
                            using (var md5 = new MD5CryptoServiceProvider())
                            {
                                var buffer = md5.ComputeHash(inStream);
                                var sb = new StringBuilder();
                                for (int j = 0; j < buffer.Length; j++)
                                {
                                    sb.Append(buffer[j].ToString("x2"));
                                }
                                string hash = sb.ToString();
                                if (hash == entry.Hash)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    string to = Path.Combine(updateDir, relPath);
                    string dir = Path.GetDirectoryName(to);
                    Directory.CreateDirectory(dir);

                    if (File.Exists(to))
                    {
                        // we can hash this too TODO
                        File.Delete(to);
                    }

                    using (WebClient client = new WebClient())
                    {
                        Console.WriteLine("Downloading " + entry.Name);
                        string url = rootUrl + relPath.Replace(Path.DirectorySeparatorChar, '/');
                        client.DownloadFile(url, to);
                    }
                }
            }
        }

        /// <summary>
        /// This method expects that you already downloaded the entire update
        /// </summary>
        /// <param name="local"></param>
        /// <param name="server"></param>
        public void DoUpdateFromServer(UpdateInfo local, UpdateInfo update)
        {
            string updateDir = Path.Combine(Root, "Update");

            for (int i = 0; i < update.Entries.Count; i++)
            {
                EntryInfo entry = update.Entries[i];

                string relPath = Path.Combine(entry.RelativePath, entry.Name);
                string to = Path.Combine(updateDir, relPath);
                string rooted = Path.Combine(Root, relPath);

                if (entry.Type == EntryType.Directory)
                {
                    Directory.CreateDirectory(to);
                    continue;
                }

                if (entry.State == EntryState.Added ||
                    entry.State == EntryState.Updated)
                {
                    if (!File.Exists(to))
                    {
                        // file was already here
                        continue;
                    }

                    string dir = Path.GetDirectoryName(to);
                    Directory.CreateDirectory(dir);

                    if (File.Exists(rooted))
                    {
                        File.Delete(rooted);
                    }

                    File.Move(to, rooted);
                }
                else if (entry.State == EntryState.Removed)
                {
                    // deleeeeete
                    if (File.Exists(rooted))
                    {
                        File.Delete(rooted);
                    }
                }
            }

            Directory.Delete(updateDir, true);
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
            Latest = Math.Max(Latest, update.Revision);

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

        [JsonIgnore]
        public string Location
        {
            get
            {
                return Path.Combine(nukeDir, ProjectInfoFile);
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

        public void Save(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
