using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;
namespace Shared.Data
{
    public interface IDataBase
    {
        public Task SaveToJsonAsync();
        public Task ReadFromJsonAsync();
    }
}
