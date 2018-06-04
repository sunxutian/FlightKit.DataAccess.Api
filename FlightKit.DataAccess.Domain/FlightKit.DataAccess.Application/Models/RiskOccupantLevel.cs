using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskOccupantLevel
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
    }
}
