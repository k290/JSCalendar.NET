﻿using Lib.JsonConfiguration;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
    public class Relation
    {
        [JsonPropertyName("@type")]
        public string Type { get; } = "Relation";

        [JsonPropertyName("relation")]
        [JsonConverter(typeof(NonStringKeyDictionaryJsonConverter<RelationType, bool>))]
        public IDictionary<RelationType, bool> Relations { get; set; } = new Dictionary<RelationType, bool>();

    }
}
