using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    public class JSEvent : IParentNode
    {
        [JsonInclude]
        public string @type { get; } = "jsevent";
    }
}
