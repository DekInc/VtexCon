using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VtexCon.Models {
    public class Category {
        ///Category ID
        [JsonProperty("id")]
        public int? Id { get; set; }
        ///Category Name
        [JsonProperty("name")]
        public string Name { get; set; }
        //ID of the father category, apply in case of category and subcategory
        [JsonProperty("fatherCategoryId")]
        public int? FatherCategoryId { get; set; }
        //Category Title
        [JsonProperty("title")]
        public string Title { get; set; }
        //Describes details about the Category
        [JsonProperty("description")]
        public string Description { get; set; }
        //Substitutes words for the Category
        [JsonProperty("keywords")]
        public string Keywords { get; set; }
        //Shows if the Category is active or not
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        //	Shows the specific code for the LomadeeCampaign
        [JsonProperty("lomadeeCampaignCode")]
        public string LomadeeCampaignCode { get; set; }
        //Shows the specific code for the AdWords remarketing platform
        [JsonProperty("adWordsRemarketingCode")]
        public string AdWordsRemarketingCode { get; set; }
        //	Shows if is on side and upper menu
        [JsonProperty("showInStoreFront")]
        public bool ShowInStoreFront { get; set; }
        //	If Category has Brand filter
        [JsonProperty("showBrandFilter")]
        public bool ShowBrandFilter { get; set; }
        //	If the Category has an active link on the website
        [JsonProperty("activeStoreFrontLink")]
        public bool ActiveStoreFrontLink { get; set; }
        //	Google Global Category ID
        [JsonProperty("globalCategoryId")]
        public int GlobalCategoryId { get; set; }
        //hows how the SKU will be exhibit
        [JsonProperty("stockKeepingUnitSelectionMode")]
        public string StockKeepingUnitSelectionMode { get; set; }
        //Score for search ordination
        [JsonProperty("score")]
        public object Score { get; set; }
        //Text Link
        [JsonProperty("linkId")]
        public string LinkId { get; set; }
        //	If the Category has a Category Child
        [JsonProperty("hasChildren")]
        public bool HasChildren { get; set; }
    }
}
