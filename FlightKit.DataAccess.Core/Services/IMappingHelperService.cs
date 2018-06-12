using FlightKit.DataAccess.Domain.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.Services
{
    /// <summary>
    /// Mapping helper service
    /// </summary>
    public interface IMappingHelperService
    {
        /// <summary>
        /// Wrapper of auto mapper Map method
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>mapped target object</returns>
        TTarget Map<TTarget>(object source);

        /// <summary>
        /// Maps the enity queryable to dto query.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="targetQueryable">The target queryable.</param>
        /// <param name="includesSyncMetadata">if set to <c>true</c> [includes synchronize metadata].</param>
        /// <returns>
        /// map queryable to dto query
        /// </returns>
        IQueryable<TDto> MapQueryableFromEntity<TSource, TDto>(IQueryable<TSource> targetQueryable, bool includesSyncMetadata = false)
            where TSource: class, new();
    }
}
