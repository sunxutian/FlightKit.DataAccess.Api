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
    public class GetRiskReportsByRiskIdCommandHandler : ICommandHandler<GetRiskReportsByRiskIdCommand, ICollection<RiskReport>>
    {
        private readonly IFlightKitReportDataService _flightKitReportDataService;

        public GetRiskReportsByRiskIdCommandHandler(IFlightKitReportDataService flightKitReportDataService)
        {
            _flightKitReportDataService = flightKitReportDataService;
        }
        public Task<ICollection<RiskReport>> HandleAsync(GetRiskReportsByRiskIdCommand command)
        {
            return _flightKitReportDataService.GetRiskReportsByRiskIdAsync(command.RiskId, command.IncludeSyncMetadata);
        }
    }
}
