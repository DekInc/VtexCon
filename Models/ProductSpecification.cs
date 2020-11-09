using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class ProductSpecification {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public List<object> FieldValueIds { get; set; }
        public List<string> FieldValues { get; set; }
        public bool IsFilter { get; set; }
        public int FieldGroupId { get; set; }
        public string FieldGroupName { get; set; }
    }
}
