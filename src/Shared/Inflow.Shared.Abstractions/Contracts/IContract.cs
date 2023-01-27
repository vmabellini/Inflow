using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Contracts
{
    public interface IContract
    {
        /// <summary>
        /// Type of contract to be validated
        /// </summary>
        Type Type { get; }
        /// <summary>
        /// Required properties to validate
        /// </summary>
        public IEnumerable<string> Required { get; }
    }
}
