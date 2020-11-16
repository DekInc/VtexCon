using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataParameterAttribute : Attribute
    {
        public string ColumnName { get; set; }

        public bool Ignore { get; set; }

        public DataParameterAttribute()
        {
        }
    }
}
