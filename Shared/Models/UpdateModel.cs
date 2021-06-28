using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class UpdateModel
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int EventId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

    }
}
