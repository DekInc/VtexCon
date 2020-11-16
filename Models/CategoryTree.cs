using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class CategoryTree {
        ///Category ID
        [JsonProperty("id")]
        public int Id { get; set; }
        ///Category Name
        [JsonProperty("name")]
        public string Name { get; set; }
        ///If the Category has a Category Child
        [JsonProperty("hasChildren")]
        public bool HasChildren { get; set; }
        ///Category URL
        [JsonProperty("url")]
        public string Url { get; set; }
        ///Category Child Object
        [JsonProperty("children")]
        public List<CategoryTree> Children { get; set; }
        ///Category page Meta Title
        [JsonProperty("title")]
        public string Title { get; set; }
        ///Category page Meta Description
        [JsonProperty("metaTagDescription")]
        public string MetaTagDescription { get; set; }
        public int? Father { get; set; }
    }
}
