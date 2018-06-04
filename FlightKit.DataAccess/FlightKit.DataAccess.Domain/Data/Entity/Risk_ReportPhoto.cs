using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_ReportPhoto : IFlightKitEntity
    {
        public Guid ReportPhotoIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public Guid? PhotoIdentifier { get; set; }
        public string ReportPhotoType { get; set; }

        public Risk_Report Report { get; set; }
    }
}
