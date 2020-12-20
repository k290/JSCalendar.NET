using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models.DataTypes
{

    public struct RelationKeys
    {
        public const string View1 = "Whatever string you like";
        public const string View2 = "another string";
    }
    public class Relation
    {
        [JsonPropertyName("@type")]
        public string @type { get; } = "Relation";
        [JsonPropertyName("relation")]
        public IDictionary<string, bool> relations { get; set; } = new Dictionary<string, bool>();
    }
}
