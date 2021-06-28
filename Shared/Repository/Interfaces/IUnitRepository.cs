using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public interface IUnitRepository:IRepository<Unit>
    {
        Unit GetUnitWithTanks(int id);
    }
}
