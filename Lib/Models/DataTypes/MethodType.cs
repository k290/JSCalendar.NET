using System.Text.Json.Serialization;

namespace Lib.Models.DataTypes
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum MethodType
    {
        [JsonPropertyName("publish")]
        Publish,
        [JsonPropertyName("request")]
        Request,
        [JsonPropertyName("reply")]
        Reply,
        [JsonPropertyName("add")]
        Add,
        [JsonPropertyName("cancel")]
        Cancel,
        [JsonPropertyName("refresh")]
        Refresh,
        [JsonPropertyName("counter")]
        Counter,
        [JsonPropertyName("declinecounter")]
        DeclineCounter,
    }
}
