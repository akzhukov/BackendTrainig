using Shared.DBContext;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public EventRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }
        public Event Create(Event item)
        {
            throw new NotImplementedException();
        }

        public Event Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Event Get(int id)
        {
            return dataBaseContext.Set<Event>().Find(id);
        }

        public IEnumerable<Event> GetByUnitId(int unitId = 1, int take = 1, int skip = 0)
        {
            if (take > 100)
            {
                throw new ArgumentOutOfRangeException("Event limit overflowed");
            }
            return dataBaseContext.Set<Event>().Where(e => e.UnitId == unitId).Skip(skip).Take(take);
        }

        public Event GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Event Update(Event item)
        {
            var oldItem = Get(item.Id);
            if (oldItem == null)
            {
                throw new InvalidOperationException($"Failed to update item. Could not find item with ID = {item.Id}");
            }

            dataBaseContext.Entry(oldItem).CurrentValues.SetValues(item);
            dataBaseContext.SaveChanges();

            return item;
        }
    }
}
