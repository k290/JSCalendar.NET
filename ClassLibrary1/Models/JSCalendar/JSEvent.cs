using FluentValidation;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    public sealed class JSEvent :  JSCommon, IParentNode, IGroupEntry
    {
        [JsonPropertyName("@type")]
        public override string @type { get; } = "jsevent";

        public string GetJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    public class JSEventValidator : AbstractValidator<JSEvent>
    {
        public JSEventValidator()
        {
            Include(new JSCommonValidator());
        }
    }
}
