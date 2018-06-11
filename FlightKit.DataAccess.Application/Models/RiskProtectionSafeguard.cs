using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskProtectionSafeguard : RiskDtoWithSyncMetadata, IFlightDtoWithReportId
    {
        public Guid ProtectionSafeguardIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ProtectionSafeguardCodeValue { get; set; }
    }
}
