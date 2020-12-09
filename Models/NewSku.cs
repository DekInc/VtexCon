using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class NewSku {
        //You can't create a new SKU as active. To set an SKU as active, it must have:
        //    At least one SKU File associated with it.
        //    At least one active component associated with it, if the SKU is set as a kit.
        //If you create an SKU with IsActive as true, it will return a 400 - Bad Request.
        //The PackagedWeightKg and WeightKg attributes are not exclusive in Kilos. It can be used in another weight unit. It is important to maintain consistency and use the same weight unit on both attributes.
        [JsonProperty("id")]
        public int? Id { get; set; }
        //Product ID
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        //Shows if the SKU is active. To avoid receiving a 400 - Bad Request this attribute must be false
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        //SKU Name
        [JsonProperty("name")]
        public string Name { get; set; }        
        [JsonProperty("refId")]
        public string RefId { get; set; }
        [JsonProperty("packagedHeight")]
        public double PackagedHeight { get; set; }
        [JsonProperty("packagedLength")]
        public double PackagedLength { get; set; }
        [JsonProperty("packagedWidth")]
        public double PackagedWidth { get; set; }
        [JsonProperty("packagedWeightKg")]
        public double PackagedWeightKg { get; set; }
        [JsonProperty("height")]
        public double Height { get; set; }
        [JsonProperty("length")]
        public double Length { get; set; }
        [JsonProperty("width")]
        public double Width { get; set; }
        [JsonProperty("weightKg")]
        public double WeightKg { get; set; }
        [JsonProperty("cubicWeight")]
        public double CubicWeight { get; set; }
        //Shows if the SKU is a Kit
        [JsonProperty("isKit")]
        public bool? IsKit { get; set; }
        //SKU Creation Date
        [JsonProperty("creationDate")]
        public string CreationDate { get; set; }
        //How much the client will get rewarded by buying the SKU
        [JsonProperty("rewardValue")]
        public double? RewardValue { get; set; }
        //SKU Estimated Date Arrival
        [JsonProperty("estimatedDateArrival")]
        public string EstimatedDateArrival { get; set; }
        [JsonProperty("manufacturerCode")]
        public string ManufacturerCode { get; set; }
        [JsonProperty("commercialConditionId")]
        public int? CommercialConditionId { get; set; }
        [JsonProperty("measurementUnit")]
        public string MeasurementUnit { get; set; }
        //Multiplies the amount of SKUs inserted on the cart
        [JsonProperty("unitMultiplier")]
        public double? UnitMultiplier { get; set; }
        [JsonProperty("modalType")]
        public string ModalType { get; set; }
        //Defines if Kit components can be sold apart
        [JsonProperty("kitItensSellApart")]
        public bool? KitItensSellApart { get; set; }
    }
}
