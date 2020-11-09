using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VtexCon.Models {
    public class Attachment {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<object> Keys { get; set; }
        public List<object> Fields { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
    }

    
}
