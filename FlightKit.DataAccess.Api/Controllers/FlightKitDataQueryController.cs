using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.UnitOfWork.CommandHandlers;
using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Api.Controllers
{
    [Route("api/query")]
    [Produces("application/json")]
    public class FlightKitDataQueryController : Controller
    {
        private readonly ICommandHandler<GetRiskReportByReportIdCommand, RiskReport> _getReportByReportIdCommandHandler;
        private readonly ICommandHandler<GetRiskReportsByRiskIdCommand, ICollection<RiskReport>> _getReportsByRiskIdCommandHandler;

        public FlightKitDataQueryController(
            ICommandHandler<GetRiskReportByReportIdCommand, RiskReport> getReportByReportIdCommandHandler,
            ICommandHandler<GetRiskReportsByRiskIdCommand, ICollection<RiskReport>> getReportsByRiskIdCommandHandler)
        {
            _getReportByReportIdCommandHandler = getReportByReportIdCommandHandler;
            _getReportsByRiskIdCommandHandler = getReportsByRiskIdCommandHandler;
        }

        [HttpGet("report/reportId/{reportId}")]
        [ProducesResponseType(200, Type = typeof(RiskReport))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRiskReportByReportIdAsync(Guid reportId)
        {
            GetRiskReportByReportIdCommand command = new GetRiskReportByReportIdCommand
            {
                ReportId = reportId,
                IncludeSyncMetadata = false
            };

            var report = await _getReportByReportIdCommandHandler.HandleAsync(command).ConfigureAwait(false);
            if (report == null)
            {
                return NotFound();
            }

            return Json(report);
        }

        [HttpGet("reports/riskId/{riskId:length(12)}")]
        [ProducesResponseType(200, Type = typeof(RiskReport))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRiskReportsByRiskIdAsync(string riskId)
        {
            GetRiskReportsByRiskIdCommand command = new GetRiskReportsByRiskIdCommand
            {
                RiskId = riskId,
                IncludeSyncMetadata = false
            };

            var reports = await _getReportsByRiskIdCommandHandler.HandleAsync(command).ConfigureAwait(false);
            if (reports?.Any() != true)
            {
                return NotFound();
            }

            return Json(reports);
        }
    }
}
