using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data;
using FlightKit.DataAccess.Domain.Helpers;
using FlightKit.DataAccess.Domain.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.Services.Impl
{
    public class MappingHelperService : IMappingHelperService
    {
        private readonly IMapper _mapper;
        public MappingHelperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTarget Map<TTarget>(object source)
        {
            return _mapper.Map<TTarget>(source);
        }

        public async Task<List<T>> GetMappedDtoFromDbAsync<TDomain, T>(IDbRepository<TDomain> repo, Expression<Func<TDomain, bool>> predicate)
            where TDomain : class, new()
        {
            var result = await repo.QueryBy(predicate).ProjectTo<T>(this._mapper.ConfigurationProvider).ToListAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<List<TDto>> GetMappedDtoFromDbWithSyncMetadataAsync<TSyncMetadataEntity, TSyncMetadataDto, TDomain, TDto, TPk>(IDbRepository<TDomain> repo,
            IDbRepository<TSyncMetadataEntity> syncMetadataRepo, Func<TDto, TPk> pkSelector,
            Expression<Func<TDomain, bool>> predicate)
            where TDomain : class, IEntityWithSyncMetadata<TSyncMetadataEntity>, new()
            where TSyncMetadataEntity : class, IServerSyncMetadata, new()
            where TSyncMetadataDto : class, ISyncMetadataDto, new()
            where TDto : class, IDtoWithSyncMetadata<TSyncMetadataDto>, new()
        {
            var dataWithoutSyncMetadata = await GetMappedDtoFromDbAsync<TDomain, TDto>(repo, predicate).ConfigureAwait(false);
            string tableName = typeof(TDomain).GetCustomAttribute<TableNameAttribute>()?.TableName
                ?? typeof(TDto).Name;
            string schemaName = typeof(TDomain).GetCustomAttribute<TableNameAttribute>()?.SchemaName;
            ICollection<TSyncMetadataDto> syncMetadata;

            switch (typeof(TPk))
            {
                case Type guidPk when guidPk == typeof(Guid):
                    syncMetadata = await GetSyncMetadatasAsync<TSyncMetadataEntity, TSyncMetadataDto>(syncMetadataRepo,
                        dataWithoutSyncMetadata.Select(pkSelector).Cast<Guid>(), tableName).ConfigureAwait(false);
                    return GetDataWithSyncMetadata(dataWithoutSyncMetadata, syncMetadata,
                        d => Guid.Parse(pkSelector(d).ToString()), s => s.GuidId).ToList();

                case Type intPk when intPk == typeof(int):
                    syncMetadata = await GetSyncMetadatasAsync<TSyncMetadataEntity, TSyncMetadataDto>(syncMetadataRepo,
                        dataWithoutSyncMetadata.Select(pkSelector).Cast<int>(), tableName).ConfigureAwait(false);
                    return GetDataWithSyncMetadata(dataWithoutSyncMetadata, syncMetadata,
                        d => int.Parse(pkSelector(d).ToString()), s => s.BigIntId).ToList();

                case Type stringPk when stringPk == typeof(string):
                    syncMetadata = await GetSyncMetadatasAsync<TSyncMetadataEntity, TSyncMetadataDto>(syncMetadataRepo,
                        dataWithoutSyncMetadata.Select(s => pkSelector(s).ToString()), tableName).ConfigureAwait(false);
                    return GetDataWithSyncMetadata(dataWithoutSyncMetadata, syncMetadata,
                        d => pkSelector(d).ToString(), s => s.StringId).ToList();
            }

            return dataWithoutSyncMetadata;
        }

        #region private methods
        private async Task<ICollection<TSyncMetadataDto>> GetSyncMetadatasAsync<TSyncMetadataEntity, TSyncMetadataDto>(
            IDbRepository<TSyncMetadataEntity> syncMetadataRepo, IEnumerable<Guid> pks, string tableName)
             where TSyncMetadataEntity : class, IServerSyncMetadata, new()
        {
            var result = await GetMappedDtoFromDbAsync<TSyncMetadataEntity, TSyncMetadataDto>(syncMetadataRepo,
                s => s.GuidId != null && pks.Contains(s.GuidId.Value) && s.SyncTable == tableName);

            return result;
        }

        private async Task<ICollection<TSyncMetadataDto>> GetSyncMetadatasAsync<TSyncMetadataEntity, TSyncMetadataDto>(
            IDbRepository<TSyncMetadataEntity> syncMetadataRepo, IEnumerable<string> pks, string tableName)
            where TSyncMetadataEntity : class, IServerSyncMetadata, new()
        {
            var result = await GetMappedDtoFromDbAsync<TSyncMetadataEntity, TSyncMetadataDto>(syncMetadataRepo,
                s => pks.Contains(s.StringId) && s.SyncTable == tableName);

            return result;
        }

        private async Task<ICollection<TSyncMetadataDto>> GetSyncMetadatasAsync<TSyncMetadataEntity, TSyncMetadataDto>(
            IDbRepository<TSyncMetadataEntity> syncMetadataRepo, IEnumerable<int> pks, string tableName)
            where TSyncMetadataEntity : class, IServerSyncMetadata, new()
        {
            var result = await GetMappedDtoFromDbAsync<TSyncMetadataEntity, TSyncMetadataDto>(syncMetadataRepo,
                s => s.BigIntId != null && pks.Contains(s.BigIntId.Value) && s.SyncTable == tableName);

            return result;
        }

        private IEnumerable<TDto> GetDataWithSyncMetadata<TDto, TSyncMetadataDto, TPk>(IEnumerable<TDto> dataWithoutSyncMetadata,
            IEnumerable<TSyncMetadataDto> syncMetadata, Func<TDto, TPk> dtoPkSelector,
            Func<TSyncMetadataDto, TPk> syncMetadataFkSelector)
            where TSyncMetadataDto : class, ISyncMetadataDto, new()
            where TDto : class, IDtoWithSyncMetadata<TSyncMetadataDto>, new()
        {
            foreach (var d in dataWithoutSyncMetadata)
            {
                var pk = dtoPkSelector(d);
                var metadata = syncMetadata.FirstOrDefault(s => syncMetadataFkSelector(s).Equals(pk));
                d.RiskSyncMetadata = metadata;
                yield return d;
            }
        }
        #endregion
    }
}
