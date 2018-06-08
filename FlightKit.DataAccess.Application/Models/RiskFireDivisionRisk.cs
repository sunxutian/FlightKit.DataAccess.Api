using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskFireDivisionRisk : IFlightDtoWithReportId, IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public Guid FireDivisionRiskIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string RiskId { get; set; }
        public RiskSyncMetadata RiskSyncMetadata { get; set; }

    }
}
