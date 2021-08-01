using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.JsonConfiguration
{
    public class NonStringKeyDictionaryJsonConverterFactory : JsonConverterFactory
    {
        private static readonly Type[] ConvertableTypes = new Type[]
        {
        typeof(bool),
        typeof(byte),
        typeof(char),
        typeof(decimal),
        typeof(double),
        typeof(float),
        typeof(int),
        typeof(long),
        typeof(sbyte),
        typeof(short),
        typeof(uint),
        typeof(ulong),
        typeof(ushort),
        };

        public static bool CanConvertImpl(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType
                && typeToConvert.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                var keyType = typeToConvert.GenericTypeArguments[0];
                return keyType.IsEnum || ConvertableTypes.Any(convertableType => keyType == convertableType);
            }

            var baseType = typeToConvert.BaseType;
            if (!(baseType is null))
            {
                return CanConvertImpl(baseType);
            }

            return false;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return CanConvertImpl(typeToConvert);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(NonStringKeyDictionaryJsonConverter<,>)
                .MakeGenericType(typeToConvert.GenericTypeArguments[0], typeToConvert.GenericTypeArguments[1]);

            var converter = (JsonConverter?)Activator.CreateInstance(
                converterType,
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                Array.Empty<object>(), //_converterOptions, _namingPolicy
                culture: null);

            return converter;
        }
    }

    //The point of this is when the enum is the dictionary key, the serializer by default doesn't fetch the JsonPropertyName
    public class NonStringKeyDictionaryJsonConverter<TKey, TValue> 
        : JsonConverter<IDictionary<TKey, TValue>> where TKey : notnull
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return NonStringKeyDictionaryJsonConverterFactory.CanConvertImpl(typeToConvert);
        }

        public override Dictionary<TKey, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IDictionary<TKey, TValue> enumKeyDictionary, JsonSerializerOptions options)
        {
            var stringKeyDictionary = new Dictionary<string, object?>(enumKeyDictionary.Count);

            foreach (var (key, value) in enumKeyDictionary)
            {
                var keyAsJson = JsonSerializer.Serialize(key, options);
                var stringKey = JsonSerializer.Deserialize<string>(keyAsJson, options)!;
                stringKeyDictionary[stringKey] = value;
            }

            JsonSerializer.Serialize(writer, stringKeyDictionary, options);
        }
    }
}
