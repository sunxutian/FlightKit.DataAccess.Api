using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlightKit.DataAccess.Core.Helpers;
using FlightKit.DataAccess.Domain.Repo;
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


        public IQueryable<TDto> MapQueryableFromEntity<TSource, TDto>(IQueryable<TSource> sourceQueryable, bool includesSyncMetadata = false)
            where TSource : class, new()
        {
            IMapper mapper = includesSyncMetadata ? _mapperWithSyncMetadata : _mapperwithoutSyncMetadata;
            var result = sourceQueryable.ProjectTo<TDto>(mapper.ConfigurationProvider);
            return result;
        }
    }
}
