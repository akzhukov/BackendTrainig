using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Shared.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Data
{
    public class DataBase : IDataBase
    {

        private string filename;
        public IList<Tank> Tanks { get; set; } = new List<Tank>();
        public IList<Factory> Factories { get; set; } = new List<Factory>();
        public IList<Unit> Units { get; set; } = new List<Unit>();

        public DataBase(string filename)
        {
            this.filename = filename;
        }

        /// <summary>
        /// save data from the database to json file
        /// </summary>
        public async Task SaveToJsonAsync()
        {
            var data = new
            {
                tanks = Tanks,
                units = Units,
                factories = Factories
            };
            await File.WriteAllTextAsync(filename, JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// Load data to the database from json file
        /// </summary>
        public async Task ReadFromJsonAsync()
        {
            if (File.Exists(filename))
            {
                string str = await File.ReadAllTextAsync(filename);

                var dbObjects = JsonConvert.DeserializeObject<DataBaseObjectsDto>(str);
                Tanks = dbObjects.Tanks;
                Factories = dbObjects.Factories;
                Units = dbObjects.Units;
            }

        }
    }
}
