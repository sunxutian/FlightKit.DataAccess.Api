using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data;
using FlightKit.DataAccess.Domain.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.Services
{
    public interface IFlightKitReportDataService
    {
        Task<RiskReport> GetRiskReportByReportIdAsync(Guid reportId, bool includesSyncMetadata = false);
        Task<ICollection<RiskReport>> GetRiskReportsByRiskIdAsync(string riskId, bool includesSyncMetadata = false);
        Task<ICollection<RiskReport>> GetRiskReportsByOrderIdAsync(long orderId, bool includesSyncMetadata = false);
        Task<ICollection<TDtoWithSyncMetadata>> GetRiskDataWithSyncMetadataByReportIdAsync<TEntity, TDtoWithSyncMetadata>(Expression<Func<Risk_Report, bool>> filter,
            Expression<Func<Risk_Report, IEnumerable<TEntity>>> getDataExp)
            where TDtoWithSyncMetadata : RiskDtoWithSyncMetadata
            where TEntity : IEntityWithSyncMetadata<Risk_SyncMetadata>;
        Task<ICollection<TDtoWithSyncMetadata>> GetRiskDataWithSyncMetadataByReportIdAsync<TEntity, TDtoWithSyncMetadata>(Expression<Func<Risk_Report, bool>> filter,
            Expression<Func<Risk_Report, TEntity>> getDataExp)
            where TDtoWithSyncMetadata : RiskDtoWithSyncMetadata
            where TEntity : IEntityWithSyncMetadata<Risk_SyncMetadata>;
    }
}
