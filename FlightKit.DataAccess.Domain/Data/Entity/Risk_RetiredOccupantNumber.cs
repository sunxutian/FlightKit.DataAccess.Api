using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "RetiredOccupantNumbers")]
    public partial class Risk_RetiredOccupantNumber : RiskEntityWithSyncMetadata, IFlightKitEntityWithReportId
    {
        public Guid RetiredOccupantNumberIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short OccupantNumber { get; set; }

        public Risk_Report Report { get; set; }
    }
}
