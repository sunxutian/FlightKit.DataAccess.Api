using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskRetiredOccupantNumber : RiskDtoWithSyncMetadata, IFlightDtoWithReportId
    {
        public Guid RetiredOccupantNumberIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short OccupantNumber { get; set; }
    }
}
