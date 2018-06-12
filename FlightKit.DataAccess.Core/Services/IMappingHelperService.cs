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
        /// Maps the queryable to dto.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="sourceQueryable">The source queryable.</param>
        /// <param name="includesSyncMetadata">if set to <c>true</c> [includes synchronize metadata].</param>
        /// <returns>map queryable to dto via querying db</returns>
        Task<List<TDto>> MapQueryable<TSource, TDto>(IQueryable<TSource> sourceQueryable, bool includesSyncMetadata = false);
    }
}
