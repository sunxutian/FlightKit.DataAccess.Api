using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using FlightKit.DataAccess.Domain.Data;
using FlightKit.DataAccess.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.UnitOfWork.CommandHandlers
{
    [Transaction(TransactionType = System.Data.IsolationLevel.Snapshot)]
    public class GetRiskSyncMetadataByRiskReportFilterCommandHandler<TEntity, TDto> :
        ICommandHandler<GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>, 
            (ICollection<TDto> data, int totalReportsCount, Guid? endReportIdCursor, bool hasNext)>
        where TEntity : RiskEntityWithSyncMetadata, new()
        where TDto : RiskDtoWithSyncMetadata
    {
        private readonly IFlightKitReportDataService _flightKitReportDataService;

        public GetRiskSyncMetadataByRiskReportFilterCommandHandler(IFlightKitReportDataService flightKitReportDataService)
        {
            _flightKitReportDataService = flightKitReportDataService;
        }

        public Task<(ICollection<TDto> data, int totalReportsCount, Guid? endReportIdCursor, bool hasNext)> HandleAsync(GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto> command)
        {
            return command.IsAccessingDataCollection ?
                _flightKitReportDataService.GetRiskDataWithSyncMetadataAsync<TEntity, TDto>(command.Filter,
                command.GetDataCollectionExp, command.LastSyncDateTime, command.OrderBy, command.IsAscending, command.StartReportId, command.TakeNumber) :
                _flightKitReportDataService.GetRiskDataWithSyncMetadataAsync<TEntity, TDto>(command.Filter,
                command.GetDataExp, command.LastSyncDateTime, command.OrderBy, command.IsAscending, command.StartReportId, command.TakeNumber);
        }
    }
}
