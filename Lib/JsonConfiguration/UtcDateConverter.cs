using Lib.Models;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.JsonConfiguration
{
    public class UtcDateConverter : JsonConverter<UtcDate>
    {
        public override UtcDate Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(
            Utf8JsonWriter writer,
            UtcDate value,
            JsonSerializerOptions options)
        {
            switch (value)
            {
                default:
                    {
                        JsonSerializer.Serialize(writer, (DateTime)value, options);
                        break;
                    }
            }
        }
    }
}
