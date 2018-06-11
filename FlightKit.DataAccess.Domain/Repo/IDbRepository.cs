using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Repo
{
    /// <summary>
    /// Entity db repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDbRepository<TEntity>
        where TEntity: class, new()
    {
        /// <summary>
        /// Gets entity from db with filter
        /// </summary>
        /// <param name="predicate">The filter.</param>
        /// <returns>all entities from db which matches the filter</returns>
        Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Queries table the by filter, this will be used by automapper queriable provider
        /// </summary>
        /// <param name="predicate">The filter.</param>
        /// <returns>IQueryable of Entity which will be used by mapping</returns>
        IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets first entity from db with filter
        /// </summary>
        /// <param name="predicate">The filter.</param>
        /// <returns>first entities from db which matches the filter</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
