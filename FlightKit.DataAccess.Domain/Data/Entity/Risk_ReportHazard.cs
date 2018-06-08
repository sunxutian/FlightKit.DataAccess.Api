using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_ReportHazard : IFlightKitEntityWithReportId
    {
        public Guid ReportHazardIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ScheduleNumber { get; set; }
        public string Comment { get; set; }

        public Risk_Report Report { get; set; }
    }
}
