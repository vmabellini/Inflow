﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Postgres
{
    internal sealed class DbContextAppInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DbContextAppInitializer> _logger;

        public DbContextAppInitializer(IServiceProvider serviceProvider, ILogger<DbContextAppInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x) && x != typeof(DbContext));

            using var scope = _serviceProvider.CreateScope();
            foreach (var dbContextType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
                if (dbContext is null)
                    continue;

                _logger.LogInformation($"Running DB context for module {dbContextType.Name}");

                await dbContext.Database.MigrateAsync(cancellationToken);
            }

            var initializers = scope.ServiceProvider.GetServices<IInitializer>();
            foreach (var initializer in initializers)
            {
                try
                {
                    _logger.LogInformation($"Running the initializer: {initializer.GetType().Name}...");
                    await initializer.InitAsync();
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, exception.Message);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
