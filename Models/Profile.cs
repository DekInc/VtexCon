using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Profile {
        [JsonProperty("cuit")]
        public string Cuit;
        [JsonProperty("account_id")]
        public string AccountId;
        [JsonProperty("phone_number")]
        public string PhoneNumber;
        [JsonProperty("vpv_code")]
        public string VpvCode;
        [JsonProperty("has_golden_vpv")]
        public bool? HasGoldenVpv;
        [JsonProperty("emission_date")]
        public string EmissionDate;
        [JsonProperty("receive_news")]
        public bool? ReceiveNews;
        [JsonProperty("is_final_consumer")]
        public bool? IsFinalConsumer;
        [JsonProperty("access_to_vital_digital")]
        public bool? AccessToVitalDigital;
        [JsonProperty("created")]
        public DateTime? Created;
        [JsonProperty("account_name")]
        public string AccountName;
        [JsonProperty("first_login")]
        public bool? FirstLogin;
    }
}
