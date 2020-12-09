using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class UpdateInventoryBySkuIdWarehouse {
        [JsonProperty("unlimitedQuantity")]
        public bool UnlimitedQuantity { get; set; }
        [JsonProperty("dateUtcOnBalanceSystem")]
        public string DateUtcOnBalanceSystem { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("timeToRefill")]
        public string TimeToRefill { get; set; }
    }
}
