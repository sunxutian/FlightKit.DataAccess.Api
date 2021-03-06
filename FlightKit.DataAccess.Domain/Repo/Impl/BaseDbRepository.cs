﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Repo.Impl
{
    public abstract class BaseDbRepository<TDbContext, TEntity> : BaseDbRepository<TDbContext>, IDbRepository<TEntity>
            where TEntity : class, new()
            where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly bool _readonly;

        public BaseDbRepository(TDbContext dbContext, bool isReadonly = false) : base(dbContext)
        {
            _dbContext = dbContext;
            _readonly = isReadonly;
        }

        protected virtual IQueryable<TEntity> GetEntityQuery()
            =>
             this._readonly
                 ? _dbContext.Set<TEntity>().AsNoTracking().AsQueryable()
                 : this._dbContext.Set<TEntity>().AsQueryable();

        public IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetEntityQuery();
            return query.Where(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetEntityQuery();
            var result = query.FirstOrDefaultAsync(predicate);
            return result;
        }

        public Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetEntityQuery().Where(predicate).ToListAsync();
        }

        public Task<int> GetCountAsync(IQueryable<TEntity> query)
        {
            return query.CountAsync();
        }
    }

    public abstract class BaseDbRepository<TDbContext> : IDbRepository
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public BaseDbRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public Task<List<T>> QueryAsync<T>(IQueryable<T> query)
        {
            return query?.ToListAsync();
        }

        public Task<int> CountAsync<T>(IQueryable<T> query)
        {
            return query.CountAsync();
        }
    }
}
