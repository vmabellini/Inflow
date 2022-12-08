using Inflow.Shared.Abstractions.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Time
{
    public class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}
