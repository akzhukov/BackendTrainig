using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TankCreateUpdateDto
    {
        public string Name { get; set; }
        public int MaxVolume { get; set; }
        public int Volume { get; set; }
    }

}
