using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskOccupantHazard : IFlightKitDto, IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public Guid OccupantHazardIdentifier { get; set; }
        public Guid OccupantIdentifier { get; set; }
        public string ScheduleNumber { get; set; }
        public string Comment { get; set; }
        public RiskSyncMetadata RiskSyncMetadata { get; set; }

    }
}
