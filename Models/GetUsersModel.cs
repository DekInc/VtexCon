using Newtonsoft.Json;
using System.Collections.Generic;

namespace VtexCon.Models {
    public class GetUsersModel {
        [JsonProperty("count")]
        public int? Count;
        [JsonProperty("next")]
        public string Next;
        [JsonProperty("previous")]
        public string Previous;
        [JsonProperty("results")]
        public List<UserModel> Results;
    }
}
