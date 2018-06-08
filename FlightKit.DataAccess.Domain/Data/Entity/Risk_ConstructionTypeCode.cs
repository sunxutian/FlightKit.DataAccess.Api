using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "ConstructionTypeCodes")]
    public partial class Risk_ConstructionTypeCode : IFlightKitEntity
    {
        public Risk_ConstructionTypeCode()
        {
            Walls = new HashSet<Risk_Wall>();
        }

        public string ConstructionTypeCodeValue { get; set; }
        public string ConstructionTypeCodeDescription { get; set; }

        public ICollection<Risk_Wall> Walls { get; set; }
    }
}
