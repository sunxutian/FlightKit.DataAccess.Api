using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskReportRelatedDate
    {
        public Guid ReportRelatedDateIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ReportDateTypeCodeValue { get; set; }
        public DateTime? ReportDateTime { get; set; }

        public RiskReportDateTypeCode ReportDateTypeCode { get; set; }
    }
}
