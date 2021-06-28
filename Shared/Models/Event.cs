using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public double StorageValue { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonConverter(typeof(InfoToStringConverter))]
        public string Tags { get; set; }
        [JsonConverter(typeof(InfoToStringConverter))]
        public string ResponsibleOperators { get; set; }

        public class InfoToStringConverter : JsonConverter<string>
        {
            public override string Read(
                ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
            {
                using (var jsonDoc = JsonDocument.ParseValue(ref reader))
                {
                    return jsonDoc.RootElement.GetRawText();
                }
            }

            public override void Write(
                Utf8JsonWriter writer, string value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value);
            }
        }
    }
}
