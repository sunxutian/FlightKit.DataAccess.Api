using FlightKit.DataAccess.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Repo.Impl
{
    public class FlightKitDbRepository<TEntity> : BaseDbRepository<FlightKitDbContext, TEntity>
        where TEntity : class, IFlightKitEntity, new()
    {
        public FlightKitDbRepository(FlightKitDbContext dbContext) : base(dbContext, false)
        {
        }
    }

    public class FlightKitReadonlyDbRepository<TEntity> : BaseDbRepository<FlightKitDbContext, TEntity>
        where TEntity : class, IFlightKitEntity, new()
    {
        public FlightKitReadonlyDbRepository(FlightKitDbContext dbContext) : base(dbContext, true)
        {
        }
    }
}
