using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services.Logging
{
    public class PerformanceTracker : IDisposable
    {
        public const long DEFAULT_TIME_LIMIT = 1500;

        private Stopwatch _stopWatch;

        public long TimeLimit { get; private set; }

        public bool Enabled { get; private set; }

        public string Operation { get; private set; }

        public PerformanceTracker(string operation)
            : this(operation, PerformanceTracker.DEFAULT_TIME_LIMIT)
        {
        }

        public PerformanceTracker(string operation, long timeLimit)
        {
            int val = 0;
            //int.TryParse(CoreClientSection.GetSection().VitalSettings.PerformanceTrackingTimeout, out val);
            this.Enabled = val != 0;

            this.Operation = operation;

            this.TimeLimit = val != 0 ? val : timeLimit;

            if (this.Enabled)
            {
                this._stopWatch = new Stopwatch();

                this._stopWatch.Start();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Enabled)
                {
                    this._stopWatch.Stop();

                    if (this._stopWatch.ElapsedMilliseconds > this.TimeLimit)
                    {
                        //Logger.Write(string.Format("La operación ||{0}|| tardó más tiempos del límite establecido ({1} milisegundos). ", this.Operation, this._stopWatch.ElapsedMilliseconds.ToString()));
                    }
                }

                GC.SuppressFinalize(this);
            }
        }
    }
}
