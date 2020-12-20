using FluentValidation;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    public sealed class JSGroup :  JSCommon, IParentNode
    {
        [JsonPropertyName("@type")]
        public override string @type { get; } = "jsgroup";


        public ICollection<IGroupEntry> entries { get; } = new List<IGroupEntry> { }; // will need to build a proper serializer https://khalidabuhakmeh.com/serialize-interface-instances-system-text-json

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string source { get; private set; }
        internal void AddEntry(IGroupEntry entry)
        {
            entries.Add(entry);
        }

        internal void AddSource(string sourceToAdd) //todo MUST be a URI
        {
            source = sourceToAdd;
        }

        public string GetJson()
        {
            return JsonSerializer.Serialize(this);
        }

    }

    public class JSGroupValidator : AbstractValidator<JSGroup>
    {
        public JSGroupValidator()
        {
            Include(new JSCommonValidator());
        }
    }
}
