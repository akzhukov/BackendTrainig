using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data
{
    public class DataBaseObjectsDto
    {
        public IList<Unit> Units { get; set; }
        public IList<Factory> Factories { get; set; }
        public IList<Tank> Tanks { get; set; }

    }
}
