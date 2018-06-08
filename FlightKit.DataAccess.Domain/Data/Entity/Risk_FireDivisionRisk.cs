using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_FireDivisionRisk : IFlightKitEntityWithReportId
    {
        public Guid FireDivisionRiskIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string RiskId { get; set; }

        public Risk_Report Report { get; set; }
    }
}
