using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "ReportHazards")]
    public partial class Risk_ReportHazard : IFlightKitEntityWithReportId, IEntityWithSyncMetadata<Risk_SyncMetadata>
    {
        public Guid ReportHazardIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ScheduleNumber { get; set; }
        public string Comment { get; set; }

        public Risk_Report Report { get; set; }
        public Risk_SyncMetadata RiskSyncMetadata { get; set; }

    }
}
