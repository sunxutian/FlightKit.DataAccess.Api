using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Api.Services
{
    public interface ITransactionFactory<TDbContext>
        where TDbContext : DbContext
    {
        Task<IDbContextTransaction> RequestTransactionAsync(IsolationLevel? isolationLevel);
        Task<bool> CommitTransactionAsync();
        Task<bool> RollbackTransactionAsync();
    }
}
