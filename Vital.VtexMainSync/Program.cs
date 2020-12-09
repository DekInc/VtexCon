using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Vital.VtexMainSync {
    static class Program {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main() {
#if DEBUG
            MainSync U = new MainSync();
            U.OnDebug();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MainSync()
            };
            ServiceBase.Run(ServicesToRun);
#endif      
        }
    }
}
