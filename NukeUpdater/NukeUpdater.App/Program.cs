﻿using Newtonsoft.Json;
using NukeUpdater.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.App
{
    static class Program
    {
        static readonly double Version = 1.0;

        static void Main(string[] args)
        {
            bool force = args.Length > 0 && args[0] == "force";

            string loc = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            loc = Environment.CurrentDirectory;
            string nukeFile = Path.Combine(loc, ProjectInfo.ProjectInfoFile);

            if (!File.Exists(nukeFile))
            {
                // go back one folder
                loc = new DirectoryInfo(loc).Parent.FullName;
                nukeFile = Path.Combine(loc, ProjectInfo.ProjectInfoFile);
            }

            if (!File.Exists(nukeFile))
            {
                Console.WriteLine("Application did not create default Nuke Updater files");
                Console.WriteLine("Can't update");
                Console.WriteLine();
                Console.WriteLine("Press ENTER to exit");
                Console.ReadLine();
                return;
            }

            ProjectInfo proj = JsonConvert.DeserializeObject<ProjectInfo>(File.ReadAllText(nukeFile));
            proj.InitializeClient(loc);

            Console.WriteLine("NukeUpdater Version " + Version.ToString("F2"));

            if (proj.FinishedUpdate)
            {
                Console.WriteLine("Contacting server....");
                ProjectInfo update = proj.GetProjectFromServer().Result;

                if (proj.FinishedUpdate) // if we didnt finish it doesnt matter if the server version is newer
                {
                    if (proj.Latest >= update.Latest && !force)
                    {
                        Console.WriteLine("Server version equal to local version");
                        Console.WriteLine("Run with force argument to force an update if desired");
                        Console.WriteLine();
                        Console.WriteLine("Press ENTER to exit");
                        Console.ReadLine();
                        return;
                    }
                }

                UpdateInfo local = proj.GetVersionFromServer(proj.Latest).Result; // get the most uptodate version of our local info
                UpdateInfo latestServer = proj.GetLatestVersionFromServer(update).Result;
                proj.DownloadUpdateFromServer(latestServer);
                proj.DoUpdateFromServer(local, latestServer);

                proj.FinishedUpdate = true;
                proj.Latest = latestServer.Revision;
                proj.Save();
            }
            else
            {
                // user started the process to update, need to finish
                Console.WriteLine("Detected an unfinished update for version " + proj.Latest);
                Console.WriteLine();

                UpdateInfo lo;
                if (proj.Latest == -1)
                {
                    // we dont know what version were in, so just go for latest
                    ProjectInfo update = proj.GetProjectFromServer().Result;
                    lo = proj.GetVersionFromServer(update.Latest).Result;
                }
                else
                {
                    lo = proj.GetVersionFromServer(proj.Latest).Result; // get the most uptodate version of our local info
                }

                proj.DownloadUpdateFromServer(lo);
                proj.DoUpdateFromServer(null, lo);

                proj.FinishedUpdate = true;
                proj.Latest = lo.Revision;
                proj.Save();
            }
        }
    }
}
