using FlightKit.DataAccess.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.Services
{
    public interface IFlightKitReportDataService
    {
        Task<RiskReport> GetRiskReportByReportId(Guid reportId);
        Task<ICollection<RiskReport>> GetRiskReportsByRiskId(string riskId);
        Task<ICollection<RiskReport>> GetRiskReportsByOrderId(long orderId);
    }
}
