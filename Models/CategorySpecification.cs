using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VtexCon.Models {
    public class CategorySpecification {
        ///Specification Name
        [JsonProperty("name")]
        public string Name { get; set; }
        //Category ID
        [JsonProperty("categoryId")]
        public int? CategoryId { get; set; }
        //Specification ID
        [JsonProperty("fieldId")]
        public string FieldId { get; set; }
        //If the specification is active
        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }
        //If is a SKU specification
        [JsonProperty("isStockKeepingUnit")]
        public bool IsStockKeepingUnit { get; set; }
    }
}
