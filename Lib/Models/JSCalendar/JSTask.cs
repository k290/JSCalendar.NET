using FluentValidation;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{
    public sealed class JSTask : JSCommon, IGroupEntry
    {

        public override string Type { get; } = "jstask";

      
    }

    public class JSTaskValidator : AbstractValidator<JSTask>
    {
        public JSTaskValidator()
        {
            Include(new JSCommonValidator());
        }
    }
}
