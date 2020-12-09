using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.ModelsOra.Model {
    public class VtexNewSku {
        //You can't create a new SKU as active. To set an SKU as active, it must have:
        //    At least one SKU File associated with it.
        //    At least one active component associated with it, if the SKU is set as a kit.
        //If you create an SKU with IsActive as true, it will return a 400 - Bad Request.
        //The PackagedWeightKg and WeightKg attributes are not exclusive in Kilos. It can be used in another weight unit. It is important to maintain consistency and use the same weight unit on both attributes.
        public decimal Id { get; set; }
        //Product ID
        public decimal ProductId { get; set; }
        //Shows if the SKU is active. To avoid receiving a 400 - Bad Request this attribute must be false
        public bool IsActive { get; set; }
        //SKU Name
        public string Name { get; set; }
        public string RefId { get; set; }
        public decimal PackagedHeight { get; set; }
        public decimal PackagedLength { get; set; }
        public decimal PackagedWidth { get; set; }
        public decimal PackagedWeightKg { get; set; }
        //SKU Creation Date
        public DateTime CreationDate { get; set; }
        public string Ean { get; set; }
        public string MeasurementUnit { get; set; }
        //Multiplies the amount of SKUs inserted on the cart
        public decimal UnitMultiplier { get; set; }
        public string ImageUrl { get; set; }

        public float Height { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float WeightKg { get; set; }
        public float CubicWeight { get; set; }
        //Shows if the SKU is a Kit
        public bool IsKit { get; set; }        
        //How much the client will get rewarded by buying the SKU
        public float RewardValue { get; set; }
        //SKU Estimated Date Arrival
        public string EstimatedDateArrival { get; set; }
        public string ManufacturerCode { get; set; }
        public decimal CommercialConditionId { get; set; }                
        public string ModalType { get; set; }
        //Defines if Kit components can be sold apart
        public bool KitItensSellApart { get; set; }
    }
}
