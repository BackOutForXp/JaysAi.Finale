//monarch v1.0
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JaysAi.Finale.Utility
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions PrettyOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string Serialize<T>(T obj, bool pretty = false)
        {
            return JsonSerializer.Serialize(obj, pretty ? PrettyOptions : null);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
