using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class SkuSeller {
        public string SellerId { get; set; }
        public int StockKeepingUnitId { get; set; }
        public string SellerStockKeepingUnitId { get; set; }
        public bool IsActive { get; set; }
        public double FreightCommissionPercentage { get; set; }
        public double ProductCommissionPercentage { get; set; }
    }
}
