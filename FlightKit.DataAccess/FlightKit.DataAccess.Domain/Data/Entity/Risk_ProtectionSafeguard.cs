using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_ProtectionSafeguard : IFlightKitEntity
    {
        public Guid ProtectionSafeguardIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ProtectionSafeguardCodeValue { get; set; }

        public Risk_Report Report { get; set; }
    }
}
