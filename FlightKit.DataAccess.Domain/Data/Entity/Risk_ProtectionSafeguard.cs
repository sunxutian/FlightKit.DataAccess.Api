﻿using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "ProtectionSafeguards")]
    public partial class Risk_ProtectionSafeguard : IFlightKitEntityWithReportId, IEntityWithSyncMetadata<Risk_SyncMetadata>
    {
        public Guid ProtectionSafeguardIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ProtectionSafeguardCodeValue { get; set; }

        public Risk_Report Report { get; set; }
        public Risk_SyncMetadata RiskSyncMetadata { get; set; }

    }
}
