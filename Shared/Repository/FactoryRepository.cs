using Shared.Data;
using Shared.DBContext;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public class FactoryRepository : BaseRepository<Factory>, IFactoryRepository
    {
        public FactoryRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
        public Factory GetFactoryWithUnits(int id)
        {
            var item = Get(id);
            dataBaseContext.Entry(item).Collection(item => item.Units).Load();
            return item;
        }

    }
}
