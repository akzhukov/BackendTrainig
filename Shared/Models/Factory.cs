using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Factory : BaseModel
    {
        public string Description { get; set; }
        public IList<Unit> Units { get; set; }

    }
}
