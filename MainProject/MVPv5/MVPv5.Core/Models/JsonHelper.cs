using System.Text.Json;

namespace MVPv5.Core.Models
{
    /// <summary>
    /// Утилитный класс для сериализации и десериализации объектов в/из JSON.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Сериализация объекта в строку JSON с настройками, совпадающими с API.
        /// </summary>
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                WriteIndented = true
            });
        }

        /// <summary>
        /// Десериализация строки JSON в объект типа T.
        /// </summary>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }
    }
} 