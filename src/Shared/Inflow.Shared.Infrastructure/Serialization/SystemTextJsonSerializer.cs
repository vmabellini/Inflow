using System.Text.Json.Serialization;
using System.Text.Json;

namespace Inflow.Shared.Infrastructure.Serialization
{
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

        public string Serialize<T>(T value) => JsonSerializer.Serialize(value, Options);

        public T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, Options);

        public object Deserialize(string value, Type type) => JsonSerializer.Deserialize(value, type, Options);
    }
}
