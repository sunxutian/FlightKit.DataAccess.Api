using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskFireDivisionRisk : RiskDtoWithSyncMetadata, IFlightDtoWithReportId
    {
        public Guid FireDivisionRiskIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string RiskId { get; set; }
    }
}
