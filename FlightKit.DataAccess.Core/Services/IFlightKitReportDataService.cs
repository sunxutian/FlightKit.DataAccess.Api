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
        Task<(ICollection<TDtoWithSyncMetadata> data, int totalReportsCount, Guid? reportCursor, bool hasNext)> GetRiskDataWithSyncMetadataAsync<TEntity, TDtoWithSyncMetadata>(Expression<Func<Risk_Report, bool>> filter,
            Expression<Func<Risk_Report, IEnumerable<TEntity>>> getDataExp, DateTime? lastSyncDateTime = null,
            Expression<Func<Risk_Report, IComparable>> orderby = null,
            bool? isascending = true, Guid? startId = null,
            int? first = null)
            where TDtoWithSyncMetadata : RiskDtoWithSyncMetadata
            where TEntity : class, IEntityWithSyncMetadata<Risk_SyncMetadata>, new();

        Task<(ICollection<TDtoWithSyncMetadata> data, int totalReportsCount, Guid? reportCursor, bool hasNext)> GetRiskDataWithSyncMetadataAsync<TEntity, TDtoWithSyncMetadata>(Expression<Func<Risk_Report, bool>> filter,
            Expression<Func<Risk_Report, TEntity>> getDataExp, DateTime? lastSyncDateTime = null,
            Expression<Func<Risk_Report, IComparable>> orderby = null,
            bool? isascending = true, Guid? startId = null,
            int? first = null)
            where TDtoWithSyncMetadata : RiskDtoWithSyncMetadata
            where TEntity : class, IEntityWithSyncMetadata<Risk_SyncMetadata>, new();
    }
}
