using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "ReportBuildingInformation")]
    public partial class Risk_ReportBuildingInformation : IFlightKitEntityWithReportId, IEntityWithSyncMetadata<Risk_SyncMetadata>
    {
        public Guid ReportBuildingInformationIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short DisplaySequence { get; set; }
        public string BuildingInformation { get; set; }
        public bool IsComment { get; set; }

        public Risk_Report Report { get; set; }
        public Risk_SyncMetadata RiskSyncMetadata { get; set; }

    }
}
