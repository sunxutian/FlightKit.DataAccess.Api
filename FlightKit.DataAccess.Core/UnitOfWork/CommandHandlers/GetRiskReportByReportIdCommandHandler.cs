using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using FlightKit.DataAccess.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.UnitOfWork.CommandHandlers
{
    [Transaction(TransactionType = System.Data.IsolationLevel.Snapshot)]
    public class GetRiskReportByReportIdCommandHandler : ICommandHandler<GetRiskReportByReportIdCommand, RiskReport>
    {
        private readonly IFlightKitReportDataService _flightKitDataService;

        public GetRiskReportByReportIdCommandHandler(IFlightKitReportDataService flightKitDataService)
        {
            _flightKitDataService = flightKitDataService;
        }

        public Task<RiskReport> HandleAsync(GetRiskReportByReportIdCommand command)
        {
            return _flightKitDataService.GetRiskReportByReportIdAsync(command.ReportId, command.IncludeSyncMetadata);
        }
    }
}
