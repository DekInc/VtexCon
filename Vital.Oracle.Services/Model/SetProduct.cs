using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services.Model {
    public class SetProduct {
        public String2Padded RefId { get; set; } = new String2Padded("", 8);
        public String2Padded Status { get; set; } = new String2Padded("", 1);
        public String2Padded ProductId { get; set; } = new String2Padded("", 8);
        public String2 Detail { get; set; } = new String2("", 3999);
    }
}
