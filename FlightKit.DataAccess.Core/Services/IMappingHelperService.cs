using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data;
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
        /// Gets the mapped dto from database with syncmetadata async.
        /// </summary>
        /// <typeparam name="TSyncMetadataEntity">The type of the synchronize metadata entity.</typeparam>
        /// <typeparam name="TSyncMetadataDto">The type of the synchronize metadata dto.</typeparam>
        /// <typeparam name="TDomain">The type of the domain.</typeparam>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <typeparam name="TPk">The type of the pk.</typeparam>
        /// <param name="repo">The repo.</param>
        /// <param name="syncMetadataRepo">The synchronize metadata repo.</param>
        /// <param name="pkSelector">The pk selector.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        Task<List<TDto>> GetMappedDtoFromDbWithSyncMetadataAsync<TSyncMetadataEntity, TSyncMetadataDto, TDomain, TDto, TPk>(IDbRepository<TDomain> repo,
            IDbRepository<TSyncMetadataEntity> syncMetadataRepo, Func<TDto, TPk> pkSelector,
            Expression<Func<TDomain, bool>> predicate)
            where TDomain : class, IEntityWithSyncMetadata<TSyncMetadataEntity>, new()
            where TSyncMetadataEntity : class, IServerSyncMetadata, new()
            where TSyncMetadataDto : class, ISyncMetadataDto, new()
            where TDto : class, IDtoWithSyncMetadata<TSyncMetadataDto>, new();

        /// <summary>
        /// Wrapper of auto mapper Map method
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>mapped target object</returns>
        TTarget Map<TTarget>(object source);
    }
}
