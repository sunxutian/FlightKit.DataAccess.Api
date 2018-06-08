using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskReportBuildingInformation : IFlightDtoWithReportId, IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public Guid ReportBuildingInformationIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short DisplaySequence { get; set; }
        public string BuildingInformation { get; set; }
        public bool IsComment { get; set; }
        public RiskSyncMetadata RiskSyncMetadata { get; set; }

    }
}
