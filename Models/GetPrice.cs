using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class GetPrice {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }
        [JsonProperty("listPrice")]
        public double? ListPrice { get; set; }
        [JsonProperty("costPrice")]
        public double? CostPrice { get; set; }
        [JsonProperty("markup")]
        public double? Markup { get; set; }
        [JsonProperty("basePrice")]
        public double? BasePrice { get; set; }
        [JsonProperty("fixedPrices")]
        public List<GetPriceFixedPrice> FixedPrices { get; set; }
    }
}
