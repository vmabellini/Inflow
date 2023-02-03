using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Serialization
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string value);
        object Deserialize(string value, Type type);
    }
}
