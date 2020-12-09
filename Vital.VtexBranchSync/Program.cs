using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Vital.VtexBranchSync {
    static class Program {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main() {
#if DEBUG
            BranchsSync U = new BranchsSync();
            U.OnDebug();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BranchsSync()
            };
            ServiceBase.Run(ServicesToRun);
#endif      
        }
    }
}
