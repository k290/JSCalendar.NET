using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    public class JSTask : IParentNode, IGroupEntry
    {

        [JsonPropertyName("@type")]
        public string @type { get; } = "jstask";
        public string uid { get; internal set; }

        public string GetJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
