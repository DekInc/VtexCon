using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class GetPriceFixedPrice {
        [JsonProperty("tradePolicyId")]
        public string TradePolicyId { get; set; }
        [JsonProperty("value")]
        public double? Value { get; set; }
        [JsonProperty("listPrice")]
        public int? ListPrice { get; set; }
        [JsonProperty("minQuantity")]
        public int? MinQuantity { get; set; }
        [JsonProperty("dateRange")]
        public GetPriceDateRange DateRange { get; set; }
    }
}
