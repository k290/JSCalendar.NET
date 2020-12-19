using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    public class JSTask : IJSCommon, IParentNode, IGroupEntry
    {

        [JsonPropertyName("@type")]
        public string @type { get; } = "jstask";

    }
}
