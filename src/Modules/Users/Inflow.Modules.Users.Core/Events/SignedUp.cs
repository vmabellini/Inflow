using Inflow.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Users.Core.Events
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;
}
