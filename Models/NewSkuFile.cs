using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class NewSkuFile {
        [JsonProperty("isMain")]
        public bool IsMain { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
