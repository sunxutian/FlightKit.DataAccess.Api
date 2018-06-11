using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "ReportRelatedDates")]
    public partial class Risk_ReportRelatedDate : RiskEntityWithSyncMetadata, IFlightKitEntityWithReportId
    {
        public Guid ReportRelatedDateIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ReportDateTypeCodeValue { get; set; }
        public DateTime? ReportDateTime { get; set; }

        public Risk_ReportDateTypeCode ReportDateTypeCode { get; set; }
        public Risk_Report Report { get; set; }
    }
}
