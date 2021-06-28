using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll(); 
        T Get(int id);
        T GetByName(string name);
        T Create(T item);
        T Update(T item); 
        T Delete(int id); 
    }
}
