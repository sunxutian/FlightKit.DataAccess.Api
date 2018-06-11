using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "AdditionDates")]
    public partial class Risk_AdditionDate : RiskEntityWithSyncMetadata, IFlightKitEntityWithReportId
    {
        public Guid AdditionDateIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public DateTime AdditionDate { get; set; }

        public Risk_Report Report { get; set; }
    }
}
