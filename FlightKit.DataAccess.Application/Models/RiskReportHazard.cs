using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskReportHazard : RiskDtoWithSyncMetadata, IFlightDtoWithReportId
    {
        public Guid ReportHazardIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ScheduleNumber { get; set; }
        public string Comment { get; set; }
    }
}
