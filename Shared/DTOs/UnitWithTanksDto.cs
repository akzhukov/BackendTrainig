using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class UnitWithTanksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<TankDto> Tanks { get; set; }

    }
}
