using NukeUpdater.OuterApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.OuterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain domain = AppDomain.CreateDomain("NukeUpdater.Inner");

            domain.Load(Resources.NukeUpdater_App);

            //ObjectHandle handle = domain.CreateInstance("NukeUpdater.App", "Program");

        }
    }
}
