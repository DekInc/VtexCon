using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class GetSkuByProductId {
        [JsonProperty("isPersisted")]
        public bool? IsPersisted { get; set; }
        [JsonProperty("isRemoved")]
        public bool? IsRemoved { get; set; }
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("productId")]
        public int? ProductId { get; set; }
        [JsonProperty("isActive")]
        public bool? IsActive { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("height")]
        public double? Height { get; set; }
        [JsonProperty("realHeight")]
        public double? RealHeight { get; set; }
        [JsonProperty("width")]
        public double? Width { get; set; }
        [JsonProperty("realWidth")]
        public double? RealWidth { get; set; }
        [JsonProperty("length")]
        public double? Length { get; set; }
        [JsonProperty("realLength")]
        public double? RealLength { get; set; }
        [JsonProperty("weightKg")]
        public double? WeightKg { get; set; }
        [JsonProperty("realWeightKg")]
        public double? RealWeightKg { get; set; }
        [JsonProperty("modalId")]
        public string ModalId { get; set; }
        [JsonProperty("refId")]
        public string RefId { get; set; }
        [JsonProperty("cubicWeight")]
        public double? CubicWeight { get; set; }
        [JsonProperty("isKit")]
        public bool? IsKit { get; set; }
        [JsonProperty("isDynamicKit")]
        public bool? IsDynamicKit { get; set; }
        [JsonProperty("internalNote")]
        public string InternalNote { get; set; }
        [JsonProperty("dateUpdated")]
        public string DateUpdated { get; set; }
        [JsonProperty("rewardValue")]
        public double? RewardValue { get; set; }
        [JsonProperty("commercialConditionId")]
        public int? CommercialConditionId { get; set; }
        [JsonProperty("estimatedDateArrival")]
        public string EstimatedDateArrival { get; set; }
        [JsonProperty("flagKitItensSellApart")]
        public bool? FlagKitItensSellApart { get; set; }
        [JsonProperty("manufacturerCode")]
        public string ManufacturerCode { get; set; }
        [JsonProperty("referenceStockKeepingUnitId")]
        public int? ReferenceStockKeepingUnitId { get; set; }
        [JsonProperty("position")]
        public int? Position { get; set; }
        [JsonProperty("editionSkuId")]
        public int? EditionSkuId { get; set; }
        [JsonProperty("approvedAdminId")]
        public int? ApprovedAdminId { get; set; }
        [JsonProperty("editionAdminId")]
        public int? EditionAdminId { get; set; }
        [JsonProperty("activateIfPossible")]
        public bool? ActivateIfPossible { get; set; }
        [JsonProperty("supplierCode")]
        public string SupplierCode { get; set; }
        [JsonProperty("measurementUnit")]
        public string MeasurementUnit { get; set; }
        [JsonProperty("unitMultiplier")]
        public double? UnitMultiplier { get; set; }
        [JsonProperty("isInventoried")]
        public bool? IsInventoried { get; set; }
        [JsonProperty("isTransported")]
        public bool? IsTransported { get; set; }
        [JsonProperty("isGiftCardRecharge")]
        public bool? IsGiftCardRecharge { get; set; }
        [JsonProperty("modalType")]
        public string ModalType { get; set; }
        [JsonProperty("isKitOptimized")]
        public bool? IsKitOptimized { get; set; }
    }
}
