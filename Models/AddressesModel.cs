using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class AddressModel {
        [JsonProperty("street")]
        public string Street;
        [JsonProperty("number")]
        public string Number;
        [JsonProperty("city")]
        public string City;
        [JsonProperty("zipcode")]
        public string ZipCode;
        [JsonProperty("state")]
        public string State;
        [JsonProperty("country")]
        public string Country;
    }
}
