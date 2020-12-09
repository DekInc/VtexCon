using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class WarehouseDock {
        [JsonProperty("dockId")]
        public string DockId;
        [JsonProperty("time")]
        public string Time;
        [JsonProperty("cost")]
        public double? Cost;
    }
}
