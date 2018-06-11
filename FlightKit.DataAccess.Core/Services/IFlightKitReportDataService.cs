using FlightKit.DataAccess.Application.Models;
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
        Task<ICollection<TDtoWithSyncMetadata>> GetRiskDataWithSyncMetadataByReportIdAsync<TDtoWithSyncMetadata>(Guid reportId)
            where TDtoWithSyncMetadata : IDtoWithSyncMetadata<RiskSyncMetadata>;
    }
}
