using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class GetInventoryPerWarehouse {
        [JsonProperty("skuId")]
        public string SkuId { get; set; }
        [JsonProperty("warehouseId")]
        public string WarehouseId { get; set; }
        [JsonProperty("dockId")]
        public string DockId { get; set; }
        [JsonProperty("totalQuantity")]
        public int TotalQuantity { get; set; }
        [JsonProperty("reservedQuantity")]
        public int ReservedQuantity { get; set; }
        [JsonProperty("availableQuantity")]
        public int AvailableQuantity { get; set; }
        [JsonProperty("isUnlimited")]
        public bool IsUnlimited { get; set; }
        [JsonProperty("salesChannel")]
        public List<string> SalesChannel { get; set; }
        [JsonProperty("deliveryChannels")]
        public List<object> DeliveryChannels { get; set; }
        [JsonProperty("timeToRefill")]
        public object TimeToRefill { get; set; }
        [JsonProperty("dateOfSupplyUtc")]
        public object DateOfSupplyUtc { get; set; }
        [JsonProperty("supplyLotId")]
        public object SupplyLotId { get; set; }
        [JsonProperty("keepSellingAfterExpiration")]
        public bool KeepSellingAfterExpiration { get; set; }
    }
}
