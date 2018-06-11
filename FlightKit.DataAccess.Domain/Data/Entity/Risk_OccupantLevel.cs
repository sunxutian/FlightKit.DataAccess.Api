using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "OccupantLevels")]
    public partial class Risk_OccupantLevel : RiskEntityWithSyncMetadata, IFlightKitEntity
    {
        public Guid OccupantLevelIdentifier { get; set; }
        public Guid OccupantIdentifier { get; set; }
        public string OccupantLevelType { get; set; }
        public int? FloorLength { get; set; }
        public int? FloorWidth { get; set; }
        public short? LevelStart { get; set; }
        public short? LevelStop { get; set; }
        public string AutomaticSprinklerType { get; set; }
        public bool? HasExtinguisher { get; set; }
        public bool? HasLimitedSupplyFireProtectionSystem { get; set; }
        public string LimitedSupplyFireProtectionSystemComment { get; set; }

        public Risk_Occupant Occupant { get; set; }
    }
}
