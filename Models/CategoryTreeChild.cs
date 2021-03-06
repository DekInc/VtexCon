﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class CategoryTreeChild {
        public int id { get; set; }
        public string name { get; set; }
        public bool hasChildren { get; set; }
        public string url { get; set; }
        public List<object> children { get; set; }
        public string Title { get; set; }
        public string MetaTagDescription { get; set; }
    }
}
