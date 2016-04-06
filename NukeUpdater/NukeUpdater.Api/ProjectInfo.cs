using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.Api
{
    public class ProjectInfo
    {
        public string Name { get; set; }

        public List<UpdateInfo> Updates;
    }
}
