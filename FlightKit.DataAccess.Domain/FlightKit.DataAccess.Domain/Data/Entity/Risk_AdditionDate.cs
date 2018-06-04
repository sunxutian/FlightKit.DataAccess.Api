using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_AdditionDate : IFlightKitEntity
    {
        public Guid AdditionDateIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public DateTime AdditionDate { get; set; }

        public Risk_Report Report { get; set; }
    }
}
