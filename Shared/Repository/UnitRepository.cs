using Shared.Data;
using Shared.DBContext;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Repository
{
    public class UnitRepository : BaseRepository<Unit>,IUnitRepository
    {
        public UnitRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
        public Unit GetUnitWithTanks(int id)
        {
            var item = Get(id);
            dataBaseContext.Entry(item).Collection(item => item.Tanks).Load();
            return item;
        }
    }
}
