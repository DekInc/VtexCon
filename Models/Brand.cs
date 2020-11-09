using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace VtexCon.Models {
    /// <summary>
    /// Brands registered in the store's Catalog.
    /// </summary>
    public class Brand {
        //Brand’s numerical identifier        
        [JsonProperty("Id")]
        public int Id { get; set; }
        //Brand’s name
        [JsonProperty("Name")]
        public string Name { get; set; }
        //Text used in meta description tag for Brand page
        [JsonProperty("Text")]
        public string Text { get; set; }
        //Substitute words for the Brand
        [JsonProperty("Keywords")]
        public string Keywords { get; set; }
        //Text used in title tag for Brand page
        [JsonProperty("SiteTitle")]
        public string SiteTitle { get; set; }
        //If true, Brand page becomes available in store
        [JsonProperty("Active")]
        public bool Active { get; set; }
        //If true, link to Brand page will be displayed in the home menu
        [JsonProperty("MenuHome")]
        public bool MenuHome { get; set; }
        //Shows the specific code for the AdWords remarketing platform
        [JsonProperty("AdWordsRemarketingCode")]
        public string AdWordsRemarketingCode { get; set; }
        //Shows the specific code for the LomadeeCampaign
        [JsonProperty("LomadeeCampaignCode")]
        public string LomadeeCampaignCode { get; set; }
        //Score for search sorting order
        [JsonProperty("Score")]
        public object Score { get; set; }
        //Brand page slug
        [JsonProperty("LinkId")]
        public string LinkId { get; set; }
    }
}
