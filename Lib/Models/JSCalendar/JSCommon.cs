﻿using FluentValidation;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{

    //todo think about using composition with the common ones. Testing is becoming repetitive
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
        public DateTime? Created { get; internal set; }

        [JsonPropertyName("updated")]
        public DateTime Updated { get; internal set; }

        public string GetJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }
        public async Task<string> GetJsonAsync()
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, GetType(), new JsonSerializerOptions { WriteIndented = true });
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public async Task<MemoryStream> GetJsonStreamAsync()
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, GetType(), new JsonSerializerOptions { WriteIndented = true });
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
            RuleFor(e => e.Updated).NotEmpty()
                .Must((context, date) => date.Kind == DateTimeKind.Utc).WithMessage("DateTime Kind for updated must be UTC");
            RuleFor(e => e.Created).Must((context, date) => date is null || date?.Kind == DateTimeKind.Utc).WithMessage("DateTime Kind for created must be UTC");
        }
    }
}
