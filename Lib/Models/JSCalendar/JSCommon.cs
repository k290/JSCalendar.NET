using FluentValidation;
using Lib.JsonConfiguration;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{


    [AbstractClassConverter(typeof(JsCommonConverter))]
    public abstract class JSCommon
    {
        [JsonPropertyName("@type")]
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
        [JsonConverter(typeof(UtcDateConverter))]
        public UtcDate? Created { get; internal set; }

        [JsonPropertyName("updated")]
        [JsonConverter(typeof(UtcDateConverter))]
        public UtcDate Updated { get; internal set; }

        [JsonPropertyName("sequence")]
        public uint Sequence { get; internal set; } = 0;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("method")]
        public MethodType? Method { get; internal set; }

        [JsonPropertyName("title")]
        public string Title { get; internal set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; internal set; } = string.Empty;

        [JsonPropertyName("descriptionContentType")]
        public string DescriptionContentType { get; internal set; } = "text/plain";

        public string GetJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        public async Task<string> GetJsonAsync()
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, this, new JsonSerializerOptions { WriteIndented = true });
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public async Task<MemoryStream> GetJsonStreamAsync()
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, this, new JsonSerializerOptions { WriteIndented = true });
            stream.Position = 0;
            return stream;
        }
    }

    public class JSCommonValidator : AbstractValidator<JSCommon>
    {
        public JSCommonValidator()
        {

            RuleFor(e => e.Type).NotEmpty();
            RuleFor(e => e.Uid).NotEmpty();
            RuleFor(e => e.Updated).NotEmpty().SetValidator(new UtcDateValidator());
            RuleFor(e => e.Created).SetValidator(new NullUtcDateValidator());


        }
    }
}
