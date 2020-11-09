using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class UserModel {
        [JsonProperty("id")]
        public int? Id;
        [JsonProperty("first_name")]
        public string FirstName;
        [JsonProperty("last_name")]
        public string LastName;
        [JsonProperty("bussiness_name")]
        public string BussinessName;
        [JsonProperty("email")]
        public string Email;
        [JsonProperty("is_active")]
        public bool? IsActive;
        [JsonProperty("is_verified")]
        public bool? IsVerified;
        [JsonProperty("profile")]
        public Profile Profile;
        [JsonProperty("groups")]
        public List<GroupModel> Groups;
        [JsonProperty("addresses")]
        public List<AddressModel> Addresses;
        [JsonProperty("branches")]
        public List<int> Branches;
    }
}
