using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace VtexCon.Models {
    /// <summary>
    /// Brands registered in the store's Catalog.
    /// </summary>
    public class Brand {
        //Brand’s numerical identifier        
        [JsonProperty("id")]
        public int Id { get; set; }
        //Brand’s name
        [JsonProperty("name")]
        public string Name { get; set; }
        //Text used in meta description tag for Brand page
        [JsonProperty("text")]
        public string Text { get; set; }
        //Substitute words for the Brand
        [JsonProperty("keywords")]
        public string Keywords { get; set; }
        //Text used in title tag for Brand page
        [JsonProperty("siteTitle")]
        public string SiteTitle { get; set; }
        //If true, Brand page becomes available in store
        [JsonProperty("active")]
        public bool Active { get; set; }
        //If true, link to Brand page will be displayed in the home menu
        [JsonProperty("menuHome")]
        public bool MenuHome { get; set; }
        //Shows the specific code for the AdWords remarketing platform
        [JsonProperty("adWordsRemarketingCode")]
        public string AdWordsRemarketingCode { get; set; }
        //Shows the specific code for the LomadeeCampaign
        [JsonProperty("lomadeeCampaignCode")]
        public string LomadeeCampaignCode { get; set; }
        //Score for search sorting order
        [JsonProperty("score")]
        public object Score { get; set; }
        //Brand page slug
        [JsonProperty("linkId")]
        public string LinkId { get; set; }
    }
}
