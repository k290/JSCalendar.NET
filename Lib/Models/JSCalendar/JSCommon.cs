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
        public abstract string Type { get; }
        [JsonPropertyName("uid")]
        public string? Uid { get; internal set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("relatedTo")]
        public IDictionary<string, Relation>? RelatedTos { get; internal set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("prodId")]
        public string? ProdId { get; internal set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("created")]
        public string? Created { get; internal set; }
    }

    public class JSCommonValidator : AbstractValidator<JSCommon>
    {
        public JSCommonValidator()
        {
            RuleFor(e => e.Type).NotEmpty();
            RuleFor(e => e.Uid).NotEmpty();
        }
    }
}
