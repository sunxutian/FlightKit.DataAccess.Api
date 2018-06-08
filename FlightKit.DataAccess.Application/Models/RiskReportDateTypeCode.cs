using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskReportDateTypeCode : IFlightKitDto
    {
        public string ReportDateTypeCodeValue { get; set; }
        public string ReportDateTypeCodeDescription { get; set; }
    }
}
