using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Shared.Services.Json.Services
{
    public class JsonService
    {
        private JsonSerializerOptions options;

        public IList<T> DeserializeFromFile<T>(string filePath)
        {

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException($"Empty filepath");
            }
            var str = File.ReadAllText(filePath);
            var deserializeResult = JsonSerializer.Deserialize<T[]>(str);
            return deserializeResult.ToList();
        }

        public IList<T> DeserializeString<T>(string str)
        {
            var deserializeResult = JsonSerializer.Deserialize<T[]>(str);
            return deserializeResult.ToList();
        }


    }
}
