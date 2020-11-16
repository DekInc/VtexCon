using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services.Repositories.Contracts
{
    public interface ILogGeneralRepository
    {
        void Write(string mensaje);
    }
}
