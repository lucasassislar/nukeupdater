using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.Api
{
    public struct EntryInfo
    {
        public int LastUpdate { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public EntryState State { get; set; }
        public EntryType Type { get; set; }

        [JsonIgnore]
        public string NameLower { get; set; }
        [JsonIgnore]
        public string RelativePathLower { get; set; }


        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Type, Name, State);
        }
    }
}
