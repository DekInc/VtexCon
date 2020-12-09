using System;

namespace Vital.VtexBranchSync {
    static public class Extensions {
        public static int ToInt(this decimal V) {
            return Convert.ToInt32(V);
        }
        public static decimal ToDecimal(this int V) {
            return Convert.ToDecimal(V);
        }
        public static int ToInt(this string V) {
            return Convert.ToInt32(V);
        }
        public static string ToIntS(this decimal V) {
            return Convert.ToInt32(V).ToString();
        }
    }
}
