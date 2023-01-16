using System.Text;
using System.Text.Json;

namespace Inflow.Shared.Infrastructure.Modules
{
    internal sealed class JsonModuleSerializer : IModuleSerializer
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public T Deserialize<T>(byte[] value) 
            => JsonSerializer.Deserialize<T>(value, SerializerOptions);


        public object Deserialize(byte[] value, Type type)
            => JsonSerializer.Deserialize(value, type, SerializerOptions);

        public byte[] Serialize<T>(T value) 
            => JsonSerializer.SerializeToUtf8Bytes(value, SerializerOptions);
    }
}
