using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VtexCon.Util {
    static public class Common {
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static public void LogInfo(string Msj) {
            string ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Log.Info($"{ThreadId} -  {Msj}");
        }
        static public void LogError(string Msj) {
            string ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Log.Error($"{ThreadId} -  {Msj}");
        }
    }
}
