using Inflow.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Postgres.Decorators
{
    [Decorator]
    internal sealed class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandler<T> _handler;
        private readonly UnitOfWorkRegistry _unitOfWorkRegistry;
        private readonly IServiceProvider _serviceProvider;

        public TransactionalCommandHandlerDecorator(ICommandHandler<T> handler, UnitOfWorkRegistry unitOfWorkRegistry,
            IServiceProvider serviceProvider)
        {
            _handler = handler;
            _unitOfWorkRegistry = unitOfWorkRegistry;
            _serviceProvider = serviceProvider;
        }

        public async Task HandleAsync(T command, CancellationToken cancellationToken = default)
        {
            var unitOfWorkType = _unitOfWorkRegistry.Resolve<T>();
            if (unitOfWorkType is null)
            {
                await _handler.HandleAsync(command, cancellationToken);
                return;
            }

            var unitOfWork = (IUnitOfWork)_serviceProvider.GetRequiredService(unitOfWorkType);
            await unitOfWork.ExecuteAsync(() => _handler.HandleAsync(command, cancellationToken));
        }
    }
}
