using NukeUpdater.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateMaker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string testFolder = @"C:\nuke";

            //UpdateInfoBuilder builder = new UpdateInfoBuilder();
            //UpdateInfo info = builder.MakeFirstUpdate(testFolder);
            //UpdateInfo sec = builder.MakeUpdate(info, testFolder);

            //int wat = -1;
            //return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
