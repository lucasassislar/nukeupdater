using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.Api
{
    public class UpdateInfo
    {
        public List<EntryInfo> Entries { get; set; }
        public int Revision { get; set; }

        public UpdateInfo()
        {
        }
    }
}
