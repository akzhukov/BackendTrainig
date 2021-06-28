using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Tank : BaseModel
    {
        public int Volume { get; set; }
        public int MaxVolume { get; set; }
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }


    }
}
