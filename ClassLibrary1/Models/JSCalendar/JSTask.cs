using FluentValidation;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    public sealed class JSTask : JSCommon, IParentNode, IGroupEntry
    {

        [JsonPropertyName("@type")]
        public override string @type { get; } = "jstask";

        public string GetJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class JSTaskValidator : AbstractValidator<JSTask>
    {
        public JSTaskValidator()
        {
            Include(new JSCommonValidator());
        }
    }
}
