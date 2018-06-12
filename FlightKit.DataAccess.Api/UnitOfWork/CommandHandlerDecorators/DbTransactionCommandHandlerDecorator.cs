using FlightKit.DataAccess.Api.Services;
using FlightKit.DataAccess.Core.UnitOfWork.CommandHandlers;
using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using FlightKit.DataAccess.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Api.UnitOfWork.CommandHandlerDecorators
{
    public class DbTransactionCommandHandlerDecorator<TDbContext, TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand
        where TDbContext : DbContext
    {
        private readonly ICommandHandler<TCommand, TResult> _decoratee;
        private readonly ITransactionFactory<TDbContext> _transactionFactory;
        private readonly IsolationLevel? _transactionType;

        public DbTransactionCommandHandlerDecorator(DecoratorContext decoratorContext, ITransactionFactory<TDbContext> transactionFactory, ICommandHandler<TCommand, TResult> decoratee)
        {
            _decoratee = decoratee;
            _transactionFactory = transactionFactory;
            _transactionType = decoratorContext.ImplementationType
                .GetCustomAttribute<TransactionAttribute>(true)?.TransactionType;
        }

        public async Task<TResult> HandleAsync(TCommand command)
        {
            try
            {
                var transaction = await _transactionFactory.RequestTransactionAsync(_transactionType).ConfigureAwait(false);
                var result = await _decoratee.HandleAsync(command).ConfigureAwait(false);
                await _transactionFactory.CommitTransactionAsync().ConfigureAwait(false);
                return result;
            }
            catch (Exception)
            {
                await _transactionFactory.RollbackTransactionAsync().ConfigureAwait(false);
                throw;
            }
        }
    }
}
