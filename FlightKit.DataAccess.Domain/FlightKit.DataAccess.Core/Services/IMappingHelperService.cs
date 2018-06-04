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
        /// Gets the mapped application object from database entity with auto mapper query provider.
        /// </summary>
        /// <typeparam name="TDomain">The type of the db object.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="repo">The entity repo.</param>
        /// <param name="predicate">filter to get entity from db</param>
        /// <returns>mapped application object of domain entity</returns>
        Task<List<T>> GetMappedDtoFromDbAsync<TDomain, T>(IDbRepository<TDomain> repo, Expression<Func<TDomain, bool>> predicate)
            where TDomain : class, new();

        /// <summary>
        /// Wrapper of auto mapper Map method
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>mapped target object</returns>
        TTarget Map<TTarget>(object source);
    }
}
