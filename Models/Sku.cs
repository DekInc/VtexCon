using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class Sku {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string NameComplete { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ProductRefId { get; set; }
    public string TaxCode { get; set; }
    public string SkuName { get; set; }
    public bool IsActive { get; set; }
    public bool IsTransported { get; set; }
    public bool IsInventoried { get; set; }
    public bool IsGiftCardRecharge { get; set; }
    public string ImageUrl { get; set; }
    public string DetailUrl { get; set; }
    public object CSCIdentification { get; set; }
    public string BrandId { get; set; }
    public string BrandName { get; set; }
    public Dimension Dimension { get; set; }
    public RealDimension RealDimension { get; set; }
    public string ManufacturerCode { get; set; }
    public bool IsKit { get; set; }
    public List<KitItem> KitItems { get; set; }
    public List<object> Services { get; set; }
    public List<object> Categories { get; set; }
    public List<Attachment> Attachments { get; set; }
    public List<object> Collections { get; set; }
    public List<SkuSeller> SkuSellers { get; set; }
    public List<int> SalesChannels { get; set; }
    public List<Image> Images { get; set; }
    public List<string> Videos { get; set; }
    public List<SkuSpecification> SkuSpecifications { get; set; }
    public List<ProductSpecification> ProductSpecifications { get; set; }
    public string ProductClustersIds { get; set; }
    public string ProductCategoryIds { get; set; }
    public int ProductGlobalCategoryId { get; set; }
    public ProductCategories ProductCategories { get; set; }
    public int CommercialConditionId { get; set; }
    public double RewardValue { get; set; }
    public AlternateIds AlternateIds { get; set; }
    public List<string> AlternateIdValues { get; set; }
    public object EstimatedDateArrival { get; set; }
    public string MeasurementUnit { get; set; }
    public double UnitMultiplier { get; set; }
    public string InformationSource { get; set; }
    public object ModalType { get; set; }
    public string KeyWords { get; set; }
    public DateTime ReleaseDate { get; set; }
    public bool ProductIsVisible { get; set; }
    public bool ShowIfNotAvailable { get; set; }
    }
}
