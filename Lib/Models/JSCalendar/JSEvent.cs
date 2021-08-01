using FluentValidation;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{
    public sealed class JSEvent :  JSCommon, IParentNode, IGroupEntry
    {
        [JsonPropertyName("@type")]
        public override string Type { get; } = "jsevent";

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

    public class JSEventValidator : AbstractValidator<JSEvent>
    {
        public JSEventValidator()
        {
            Include(new JSCommonValidator());
        }
    }
}
