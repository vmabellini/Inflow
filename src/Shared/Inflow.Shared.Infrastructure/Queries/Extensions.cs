﻿using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Queries
{
    internal static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services, IList<System.Reflection.Assembly> assemblies)
        {
            services
                .AddSingleton<IQueryDispatcher, QueryDispatcher>()
                .Scan(x => x.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                        .WithoutAttribute<DecoratorAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
