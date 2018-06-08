using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskRetiredOccupantNumber : IFlightDtoWithReportId, IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public Guid RetiredOccupantNumberIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short OccupantNumber { get; set; }
        public RiskSyncMetadata RiskSyncMetadata { get; set; }

    }
}
