using Inflow.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Commands
{
    internal record CreateCustomer(string Email) : ICommand;
}
