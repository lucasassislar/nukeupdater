﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.Api
{
    public class EntryInfo
    {
        public int LastUpdate { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public string RelativePath { get; set; }
    }
}
