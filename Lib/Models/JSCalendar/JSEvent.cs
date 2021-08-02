using FluentValidation;
using Lib.JsonConfiguration;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{
    public sealed class JSEvent :  JSCommon, IGroupEntry
    {
        [JsonPropertyName("@type")]
        public override string Type { get; } = "jsevent";

    }

    public class JSEventValidator : AbstractValidator<JSEvent>
    {
        public JSEventValidator()
        {
            Include(new JSCommonValidator());
        }
    }
}
