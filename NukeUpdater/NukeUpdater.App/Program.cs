using Newtonsoft.Json;
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
    class Program
    {
        static double Version = 1.0;

        static void Main(string[] args)
        {
            bool force = args.Length > 0 && args[0] == "force";

            string loc = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string nukeFile = Path.Combine(loc, ProjectInfo.ProjectInfoFile);

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
            proj.Initialize(loc, true);

            Console.WriteLine("NukeUpdater Version " + Version.ToString("F2"));

            if (!proj.FinishedUpdate)
            {
                // user started the process to update, need to finish
                Console.WriteLine("Detected an unfinished update for version " + proj.Latest);
                Console.WriteLine();

                UpdateInfo lo = proj.GetVersionFromServer(proj.Latest).Result; // get the most uptodate version of our local info
                proj.DoUpdateFromServer(null, lo);

                return;
            }

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
            proj.DoUpdateFromServer(local, latestServer);
        }
    }
}
