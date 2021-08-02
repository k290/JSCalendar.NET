using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{
    public sealed class JSGroup : JSCommon, IParentNode
    {
        [JsonPropertyName("@type")]
        public override string Type { get; } = "jsgroup";


        [JsonPropertyName("entries")]
        public ICollection<IGroupEntry> Entries { get; } = new List<IGroupEntry> { }; // will need to build a proper serializer https://khalidabuhakmeh.com/serialize-interface-instances-system-text-json

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("source")]
        public string? Source { get; internal set; }


    }

    public class JSGroupValidator : AbstractValidator<JSGroup>
    {
        public JSGroupValidator()
        {
            RuleFor(x => x.Source).Must(s => Uri.IsWellFormedUriString(s, UriKind.Absolute)).When(x => x.Source != null);
            Include(new JSCommonValidator());
        }
    }
}
