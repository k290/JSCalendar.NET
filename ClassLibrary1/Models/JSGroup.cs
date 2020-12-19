using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    public class JSGroup : IParentNode
    {
        [JsonPropertyName("@type")]
        public string @type { get; } = "jsgroup";
        public string uid { get; internal set; }


        [JsonInclude]
        public ICollection<IGroupEntry> entries { get; } = new List<IGroupEntry> { }; // will need to build a proper serializer https://khalidabuhakmeh.com/serialize-interface-instances-system-text-json

        public void AddEntry(IGroupEntry entry)
        {
            entries.Add(entry);
        }

    }
}
