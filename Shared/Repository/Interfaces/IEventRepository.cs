using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetByUnitId(int unitId = 1, int take = 1, int skip = 0);
        Event Get(int id);
        Event GetByName(string name);
        Event Create(Event item);
        Event Update(Event item);
        Event Delete(int id);
    }
}
