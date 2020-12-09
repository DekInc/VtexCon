﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vital.Oracle.Services.Model;
using System.Diagnostics;

namespace Vital.Oracle.Services {
    static public class Common {
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static public void LogInfo(string Msj) {
            string ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Log.Info($"{ThreadId} -  {Msj}");
            Debug.WriteLine($"{ThreadId} -  {Msj}");
            ApiOracle ApiOracleO = new ApiOracle();
            ApiOracleO.WriteOraLog(2, (new String2($"{ThreadId} - {Msj}", 4000)).Get());
        }
        static public void LogError(string Msj) {
            string ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Log.Error($"{ThreadId} -  {Msj}");
            Debug.WriteLine($"{ThreadId} -  {Msj}");
            ApiOracle ApiOracleO = new ApiOracle();
            ApiOracleO.WriteOraLog(2, (new String2($"{ThreadId} - {Msj}", 4000)).Get());
        }
    }
}
