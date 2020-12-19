using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    public class JSEvent 
    {
        [JsonInclude]
        public string @type { get; } = "jsevent";
    }
}
