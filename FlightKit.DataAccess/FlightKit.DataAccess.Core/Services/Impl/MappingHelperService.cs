using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;
        public MappingHelperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTarget Map<TTarget>(object source)
        {
            return _mapper.Map<TTarget>(source);
        }

        public async Task<List<T>> GetMappedDtoFromDbAsync<TDomain, T>(IDbRepository<TDomain> repo, Expression<Func<TDomain, bool>> predicate) where TDomain : class, new()
        {
            var result = await repo.QueryBy(predicate).ProjectTo<T>(this._mapper.ConfigurationProvider).ToListAsync().ConfigureAwait(false);
            return result;
        }
    }
}
