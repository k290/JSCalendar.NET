using FluentValidation;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{
    public abstract class JSCommon
    {
        [JsonIgnore]
        public abstract string type { get; }
        public string? uid { get; internal set; } //todo try change to protected set
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("relatedTo")]
        public IDictionary<string, Relation>? relatedTos { get; internal set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? prodId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? created { get; set; }
    }

    public class JSCommonValidator : AbstractValidator<JSCommon>
    {
        public JSCommonValidator()
        {
            RuleFor(e => e.type).NotEmpty();
            RuleFor(e => e.uid).NotEmpty();
        }
    }
}
