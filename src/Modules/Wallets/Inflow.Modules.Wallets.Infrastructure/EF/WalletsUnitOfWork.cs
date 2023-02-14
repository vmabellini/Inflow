using Inflow.Shared.Infrastructure.Postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Wallets.Infrastructure.EF
{
    internal class WalletsUnitOfWork : PostgresUnitOfWork<WalletsDbContext>
    {
        public WalletsUnitOfWork(WalletsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
