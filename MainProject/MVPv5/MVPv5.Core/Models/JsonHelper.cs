using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVPv5.Core.Models
{
    /// <summary>
    /// Кастомный конвертер для object, для корректной десериализации значения в Dictionary<string, object>.
    /// </summary>
    public class ObjectToInferredTypesConverter : JsonConverter<object>
    {
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.Number:
                    if (reader.TryGetInt64(out long l))
                        return l;
                    return reader.GetDouble();
                case JsonTokenType.String:
                    if (reader.TryGetDateTime(out DateTime datetime))
                        return datetime;
                    return reader.GetString();
                case JsonTokenType.StartObject:
                    return JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options)!;
                case JsonTokenType.StartArray:
                    return JsonSerializer.Deserialize<List<object>>(ref reader, options)!;
                case JsonTokenType.Null:
                    return null;
                default:
                    throw new JsonException($"Unexpected token {reader.TokenType}");
            }
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value?.GetType() ?? typeof(object), options);
        }
    }

    /// <summary>
    /// Утилитный класс для сериализации и десериализации объектов в/из JSON.
    /// </summary>
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            Converters = { new ObjectToInferredTypesConverter() }
        };

        /// <summary>
        /// Сериализация объекта в строку JSON с настройками, совпадающими с API.
        /// </summary>
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, _options);
        }

        /// <summary>
        /// Десериализация строки JSON в объект типа T.
        /// </summary>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _options)!;
        }
    }
} 