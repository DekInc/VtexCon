using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services.Model {
    public class String2 {
        public string V { get; set; }
        int Max { get; set; }
        public String2(string _V, int _Max) {
            V = _V;
            Max = _Max;
        }
        public string Get() {
            if (V.Length > Max)
                return V.Substring(0, Max);
            else return V;
        }
    }
    public class String2Padded {
        public string V { get; set; }
        int Max { get; set; }
        public String2Padded(string _V, int _Max) {
            V = _V;
            Max = _Max;
        }
        public string Get() {
            if (V.Length < Max)
                return V.PadLeft(Max, ' ');
            else
                return V;
        }
    }
}
