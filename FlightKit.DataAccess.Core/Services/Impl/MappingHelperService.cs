using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlightKit.DataAccess.Core.Helpers;
using FlightKit.DataAccess.Domain.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.Services.Impl
{
    public class MappingHelperService : IMappingHelperService
    {
        private readonly IMapper _mapperwithoutSyncMetadata;
        private readonly IMapper _mapperWithSyncMetadata;

        public MappingHelperService(IMapper mapperwithoutSyncMetadata, [QueryWithSyncMetadata]IMapper mapperWithSyncMetadata)
        {
            _mapperwithoutSyncMetadata = mapperwithoutSyncMetadata;
            _mapperWithSyncMetadata = mapperWithSyncMetadata;
        }


        public TTarget Map<TTarget>(object source)
        {
            return _mapperwithoutSyncMetadata.Map<TTarget>(source);
        }

        public async Task<List<T>> GetMappedDtoFromDbAsync<TDomain, T>(IDbRepository<TDomain> repo, 
            Expression<Func<TDomain, bool>> predicate, bool includesSyncMetadata = false) where TDomain : class, new()
        {
            IMapper mapper = includesSyncMetadata ? _mapperWithSyncMetadata : _mapperwithoutSyncMetadata;

            var result = await repo.QueryBy(predicate).ProjectTo<T>(mapper.ConfigurationProvider).ToListAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<List<TDto>> MapQueryable<TSource, TDto>(IQueryable<TSource> sourceQueryable, bool includesSyncMetadata = false)
        {
            IMapper mapper = includesSyncMetadata ? _mapperWithSyncMetadata : _mapperwithoutSyncMetadata;
            var result = await sourceQueryable.ProjectTo<TDto>(mapper.ConfigurationProvider).ToListAsync().ConfigureAwait(false);

            return result;
        }
    }
}
