using Lib.JsonConfiguration;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models.DataTypes
{
    public class Relation
    {
        [JsonPropertyName("@type")]
        public string Type { get; } = "Relation";

        [JsonPropertyName("relation")]
        [JsonConverter(typeof(NonStringKeyDictionaryJsonConverter<RelationType, bool>))]
        public IDictionary<RelationType, bool> Relations { get; set; } = new Dictionary<RelationType, bool>();

    }
}
