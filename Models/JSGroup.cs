using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    public class JSGroup : IParentNode
    {
        [JsonInclude]
        [JsonPropertyName("@type")]
        public string @type { get; } = "jsgroup";

        [JsonInclude]
        public ICollection<IGroupEntry> entries { get; } = new List<IGroupEntry> { };

        public void AddEntry(IGroupEntry entry)
        {
            entries.Add(entry);
        }
    }
}
