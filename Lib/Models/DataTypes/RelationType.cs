using System.Text.Json.Serialization;

namespace Lib.Models.DataTypes
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum RelationType
    {
        [JsonPropertyName("first")]
        First,
        [JsonPropertyName("next")]
        Next,
        [JsonPropertyName("child")]
        Child,
        [JsonPropertyName("parent")]
        Parent        
    }
}
