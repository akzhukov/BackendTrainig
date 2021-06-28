using Shared.Data;
using Shared.DBContext;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public class TankRepository : BaseRepository<Tank>, ITankRepository
    {
        public TankRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }

        public async Task<int> RandomVolumeUpdate(int id)
        {
            var tank = Get(id);
            var random = new Random();

            var maxChange = (int)(tank.Volume * 0.1);
            tank.Volume += random.Next( maxChange, maxChange);
            if (tank.Volume < 0)
                tank.Volume = 0;
            await dataBaseContext.SaveChangesAsync();
            return tank.Volume;
        }
    }
}
