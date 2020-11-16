using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services.Logging
{
    public static class Logger
    {
        private static string path = ConfigurationManager.AppSettings["PathLog"];

        public static void Write(string modulo, string tipo, string mensaje)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (StreamWriter file = File.AppendText(path + "@" + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year))
                {
                    file.WriteLine(DateTime.Now.ToString() + "***[" + modulo + "]***" + tipo + ": " + mensaje);
                }
            }
            catch (Exception)
            {
                //Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
