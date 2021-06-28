using Dapper;
using Shared.DBContext;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly DataBaseContext dataBaseContext;

        public BaseRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public T Create(T item)
        {
            dataBaseContext.Set<T>().Add(item);
            dataBaseContext.SaveChanges();
            return item;
        }

        public T Delete(int id)
        {
            var item = Get(id);
            if (item == null)
            {
                return null;
            }

            dataBaseContext.Set<T>().Remove(item);
            dataBaseContext.SaveChanges();
            return item;
        }

        public T Get(int id)
        {
            return dataBaseContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dataBaseContext.Set<T>();
        }

        public T GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public T Update(T item)
        {
            var oldItem = Get(item.Id);
            if (oldItem == null)
            {
                throw new InvalidOperationException($"Failed to update item. Could not find item with ID = {item.Id}");
            }

            dataBaseContext.Entry(oldItem).CurrentValues.SetValues(item);
            dataBaseContext.SaveChanges();

            return item;


        }

    }
}
