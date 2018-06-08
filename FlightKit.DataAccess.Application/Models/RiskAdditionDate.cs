using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskAdditionDate : IFlightDtoWithReportId, IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public Guid AdditionDateIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public DateTime AdditionDate { get; set; }
        public RiskSyncMetadata RiskSyncMetadata { get; set; }
    }
}
