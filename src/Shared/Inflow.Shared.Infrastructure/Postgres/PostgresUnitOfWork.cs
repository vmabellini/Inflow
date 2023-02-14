﻿using Microsoft.EntityFrameworkCore;

namespace Inflow.Shared.Infrastructure.Postgres
{
    public class PostgresUnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly T _dbContext;

        public PostgresUnitOfWork(T dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
