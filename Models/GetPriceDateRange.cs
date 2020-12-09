using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class GetPriceDateRange {
        [JsonProperty("from")]
        public DateTime? From { get; set; }
        [JsonProperty("to")]
        public DateTime? To { get; set; }
    }
}
