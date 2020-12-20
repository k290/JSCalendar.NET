using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    public class JSGroup :  JSCommon, IParentNode
    {
        [JsonPropertyName("@type")]
        public override string @type { get; } = "jsgroup";


        [JsonInclude]
        public ICollection<IGroupEntry> entries { get; } = new List<IGroupEntry> { }; // will need to build a proper serializer https://khalidabuhakmeh.com/serialize-interface-instances-system-text-json

        internal void AddEntry(IGroupEntry entry)
        {
            entries.Add(entry);
        }

        public string GetJson()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
