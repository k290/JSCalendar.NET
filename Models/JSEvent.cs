using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    public class JSEvent : IParentNode, IGroupEntry
    {
        [JsonInclude]
        [JsonPropertyName("@type")]
        public string @type { get; } = "jsevent";
    }
}
