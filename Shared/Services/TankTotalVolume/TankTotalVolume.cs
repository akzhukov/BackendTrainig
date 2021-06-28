using Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.TankTotalVolume
{
    public class TankTotalVolume : ITankTotalVolume
    {
        ITankRepository db;
        public TankTotalVolume(ITankRepository db)
        {
            this.db = db;
        }
        public int TotalVolume()
        {
            return db.GetAll().Sum(tank => tank.Volume);
        }
    }
}
