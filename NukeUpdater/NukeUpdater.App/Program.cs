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
            ProjectInfo proj = new ProjectInfo(loc, true);

            if (!proj.Created)
            {
                Console.WriteLine("Application did not create default Nuke Updater files");
                Console.WriteLine("Can't update");
                Console.WriteLine();
                Console.WriteLine("Press ENTER to exit");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("NukeUpdater Version " + Version.ToString("F2"));
            Console.WriteLine("Contacting server....");

            UpdateInfo latestLocal = proj.ReadUpdate(proj.Latest);
            UpdateInfo update = proj.GetLatestFromServer().Result;

            if (latestLocal.Revision >= update.Revision && !force)
            {
                Console.WriteLine("Server version equal to local version");
                Console.WriteLine("Run with force argument to force an update if desired");
                Console.WriteLine();
                Console.WriteLine("Press ENTER to exit");
                Console.ReadLine();
                return;
            }

            proj.DoUpdateFromServer(latestLocal, update);
        }
    }
}
