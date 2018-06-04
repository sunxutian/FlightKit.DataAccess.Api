using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskReportPhoto
    {
        public Guid ReportPhotoIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public Guid? PhotoIdentifier { get; set; }
        public string ReportPhotoType { get; set; }
    }
}
