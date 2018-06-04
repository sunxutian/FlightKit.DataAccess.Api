using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_OccupantHazard : IFlightKitEntity
    {
        public Guid OccupantHazardIdentifier { get; set; }
        public Guid OccupantIdentifier { get; set; }
        public string ScheduleNumber { get; set; }
        public string Comment { get; set; }

        public Risk_Occupant Occupant { get; set; }
    }
}
