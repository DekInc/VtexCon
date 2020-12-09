using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.ModelsOra.Model {
    public class VtexProduct {
        public decimal Id { get; set; }
        //Product Name
        public string Name { get; set; }
        //AProduct Department ID
        public decimal DepartmentId { get; set; }
        //Product Category ID
        public decimal CategoryId { get; set; }
        //Product Brand ID
        public decimal BrandId { get; set; }
        //Text Link
        public string LinkId { get; set; }
        //Product Referecial Code
        public string RefId { get; set; }
        //If the Product is visible on the store
        public bool IsVisible { get; set; }
        //Product Description
        public string Description { get; set; }
        //Complement Name
        public string DescriptionShort { get; set; }
        //Product Release Date
        public DateTime ReleaseDate { get; set; }
        //Substitutes words for the Product
        public string KeyWords { get; set; }
        //Tag Title
        public string Title { get; set; }
        //If the Product is active or not
        public bool IsActive { get; set; }
        //Product Fiscal Code
        public string TaxCode { get; set; }
        //Meta Tag Description
        public string MetaTagDescription { get; set; }
        //Product Supplier ID
        public decimal? SupplierId { get; set; }
        //Defines if the Product will remain being shown in the store even if it’s out of stock
        public bool ShowWithoutStock { get; set; }
        //Code specific for AdWords Remarketing
        public string AdWordsRemarketingCode { get; set; }
        //Code specific for Lomadee Campaign
        public string LomadeeCampaignCode { get; set; }
        //Value used for Product search ordenation
        public decimal Score { get; set; }
        public decimal factor { get; set; }
        public decimal uxb { get; set; }
    }
}
