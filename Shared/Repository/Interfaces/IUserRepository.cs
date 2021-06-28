using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User GetByName(string name);
        User Create(User item);
        User Update(User item);
        User Delete(int id);
    }
}
