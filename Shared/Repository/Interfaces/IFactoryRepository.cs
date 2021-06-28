using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public interface IFactoryRepository : IRepository<Factory>
    {
        Factory GetFactoryWithUnits(int id);
    }
}
