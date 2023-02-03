using Inflow.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Postgres
{
    public static class Extensions
    {
        public static Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, IPagedQuery query,
        CancellationToken cancellationToken = default)
        => data.PaginateAsync(query.Page, query.Results, cancellationToken);

        public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int results,
            CancellationToken cancellationToken = default)
        {
            if (page <= 0)
            {
                page = 1;
            }

            results = results switch
            {
                <= 0 => 10,
                > 100 => 100,
                _ => results
            };

            var totalResults = await data.CountAsync();
            var totalPages = totalResults <= results ? 1 : (int)Math.Floor((double)totalResults / results);
            var result = await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);

            return new Paged<T>(result, page, results, totalPages, totalResults);
        }

        public static Task<List<T>> SkipAndTakeAsync<T>(this IQueryable<T> data, IPagedQuery query,
            CancellationToken cancellationToken = default)
            => data.SkipAndTakeAsync(query.Page, query.Results, cancellationToken);

        public static async Task<List<T>> SkipAndTakeAsync<T>(this IQueryable<T> data, int page, int results,
            CancellationToken cancellationToken = default)
        {
            if (page <= 0)
            {
                page = 1;
            }

            results = results switch
            {
                <= 0 => 10,
                > 100 => 100,
                _ => results
            };

            return await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);
        }

        internal static IServiceCollection AddPostgresOptions(this IServiceCollection services)
        {
            var options = services.GetOptions<PostgresOptions>("postgres");
            services.AddSingleton(options);
            services.AddHostedService<DbContextAppInitializer>();

            return services;
        }

        public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<PostgresOptions>("postgres");
            services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));

            return services;
        }
    }
}
