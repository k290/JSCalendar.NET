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
        public override string @type { get; } = "jsgroup";


        public ICollection<IGroupEntry> entries { get; } = new List<IGroupEntry> { }; // will need to build a proper serializer https://khalidabuhakmeh.com/serialize-interface-instances-system-text-json

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? source { get; private set; }
        internal void AddEntry(IGroupEntry entry)
        {
            entries.Add(entry);
        }

        //these methods are silly. The private set is a setter
        internal void AddSource(string sourceToAdd) //todo MUST be a URI
        {
            source = sourceToAdd;
        }

        //Move all json getters to parent 
        public string GetJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        public async Task<string> GetJsonAsync()
        {
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, this, new JsonSerializerOptions { WriteIndented = true });
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
        }

        public async Task<MemoryStream> GetJsonStreamAsync()
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, this, new JsonSerializerOptions { WriteIndented = true });
            stream.Position = 0;
            return stream;
        }

    }

    public class JSGroupValidator : AbstractValidator<JSGroup>
    {
        public JSGroupValidator()
        {
            RuleFor(x => x.source).Must(s => Uri.IsWellFormedUriString(s, UriKind.Absolute)).When(x => x.source != null);
            Include(new JSCommonValidator());
        }
    }
}
