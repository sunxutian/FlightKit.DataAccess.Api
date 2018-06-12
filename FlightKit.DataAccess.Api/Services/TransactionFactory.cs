using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FlightKit.DataAccess.Api.Services
{
    public class TransactionFactory<TDbContext> : ITransactionFactory<TDbContext>
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private IDbContextTransaction _currentTransaction;
        private SemaphoreSlim _semaphoreSlim;
        private int _commandHandlersUsingTransaction = 0;

        public TransactionFactory(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _semaphoreSlim = new SemaphoreSlim(1, 1);
        }

        public async Task<bool> CommitTransactionAsync()
        {
            try
            {
                await _semaphoreSlim.WaitAsync().ConfigureAwait(false);
                if (_currentTransaction == null || _commandHandlersUsingTransaction <= 0)
                {
                    return false;
                }

                Interlocked.Decrement(ref _commandHandlersUsingTransaction);

                System.Diagnostics.Debug.WriteLine($"commit transaction, current count {_commandHandlersUsingTransaction}");

                if (_commandHandlersUsingTransaction == 0)
                {
                    _currentTransaction.Commit();
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                    System.Diagnostics.Debug.WriteLine($"dispose transaction");
                }

                return true;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<IDbContextTransaction> RequestTransactionAsync(IsolationLevel? isolationLevel)
        {
            try
            {
                isolationLevel = isolationLevel ?? IsolationLevel.Snapshot;
                await _semaphoreSlim.WaitAsync().ConfigureAwait(false);
                if (_currentTransaction == null)
                {
                    System.Diagnostics.Debug.WriteLine($"create transaction");

                    _currentTransaction = await _dbContext.Database
                        .BeginTransactionAsync(isolationLevel ?? IsolationLevel.Snapshot);
                }

                var dbIsolation = _currentTransaction.GetDbTransaction().IsolationLevel;
                // requested isolation is more stricted than current isolation
                if (isolationLevel < dbIsolation)
                {
                    throw new InvalidOperationException("there is a transaction in use and requested isolation level is more strict than the current isolation level");
                }

                Interlocked.Increment(ref _commandHandlersUsingTransaction);
                System.Diagnostics.Debug.WriteLine($"requested transaction, current count {_commandHandlersUsingTransaction}");
                return _currentTransaction;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> RollbackTransactionAsync()
        {
            try
            {
                await _semaphoreSlim.WaitAsync().ConfigureAwait(false);
                if (_currentTransaction == null || _commandHandlersUsingTransaction <= 0)
                {
                    return false;
                }

                _commandHandlersUsingTransaction = 0;
                _currentTransaction.Rollback();
                _currentTransaction.Dispose();
                _currentTransaction = null;

                return true;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
