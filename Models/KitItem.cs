using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class KitItem {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Amount { get; set; }
        public object EstimatedDateArrival { get; set; }
        public Dimension2 Dimension { get; set; }
        public string RefId { get; set; }
        public string EAN { get; set; }
        public bool IsKitOptimized { get; set; }
    }

    
}
