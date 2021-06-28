using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Unit : BaseModel
    {
        public int? FactoryId { get; set; }
        public Factory Factory { get; set; }
        public IList<Tank> Tanks { get; set; }
    }
}
