using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskProtectionSafeguard : IFlightDtoWithReportId, IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public Guid ProtectionSafeguardIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ProtectionSafeguardCodeValue { get; set; }
        public RiskSyncMetadata RiskSyncMetadata { get; set; }

    }
}
