using FluentValidation;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    public class JSEvent :  IParentNode, IGroupEntry
    {
        [JsonPropertyName("@type")]
        public string @type { get; } = "jsevent";
        public string uid { get; internal set; }
    }

    public class JSEventValidator : AbstractValidator<JSEvent>
    {
        public JSEventValidator()
        {
            RuleFor(e => e.type).NotEmpty();
            RuleFor(e => e.uid).NotEmpty();
        }
    }
}
