using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.ModelsOra.Model {
    public class VtexCatalog {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}
