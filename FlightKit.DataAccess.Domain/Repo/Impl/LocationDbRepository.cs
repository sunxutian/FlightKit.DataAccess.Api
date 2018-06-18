using FlightKit.DataAccess.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Repo.Impl
{
    public class LocationDbRepository<TDbContext, TEntity> : BaseDbRepository<TDbContext, TEntity>
        where TEntity : class, new()
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        public LocationDbRepository(TDbContext dbContext) : base(dbContext, false)
        {
            _dbContext = dbContext;
        }

        protected override IQueryable<TEntity> GetEntityQuery()
        {
            var locationPropertyAttribute = typeof(TEntity).GetCustomAttribute<HasLocationDataAttribute>();
            if (locationPropertyAttribute == null)
            {
                return base.GetEntityQuery();
            }

            var mapping = _dbContext.Model.FindEntityType(typeof(TEntity)).Relational();
            var schema = mapping.Schema;
            var tableName = mapping.TableName;

            return base.GetEntityQuery().FromSql($"SELECT *, " +
                $"[{locationPropertyAttribute.LocationColumnName}].Lat as [{locationPropertyAttribute.LocationLatPropertyName}]," +
                $"[{locationPropertyAttribute.LocationColumnName}].Long as [{locationPropertyAttribute.LocationLongPropertyName}] " +
                $"FROM [{schema}].[{tableName}]");
        }
    }
}
