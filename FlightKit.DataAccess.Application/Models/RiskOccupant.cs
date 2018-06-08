using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskOccupant : IFlightDtoWithReportId, IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public RiskOccupant()
        {
            OccupantHazards = new HashSet<RiskOccupantHazard>();
            OccupantLevels = new HashSet<RiskOccupantLevel>();
        }

        public Guid OccupantIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short? OccupantNumber { get; set; }
        public string OccupantName { get; set; }
        public string SuiteNumber { get; set; }
        public string ScheduleNumber { get; set; }
        public short? CombustibilityClass { get; set; }
        public string BasicGroup2Symbol { get; set; }
        public string CommercialStatisticalPlan { get; set; }
        public bool? IsCombustibilityClassOverride { get; set; }
        public bool? IsBaseGroupIioverride { get; set; }
        public bool? IsCommercialStatisticalPlanOverride { get; set; }
        public short? SusceptibilityClass { get; set; }
        public bool IsSusceptibilityClassOverride { get; set; }
        public short? SortOrder { get; set; }
        public string OccupantComment { get; set; }
        public ICollection<RiskOccupantHazard> OccupantHazards { get; set; }
        public ICollection<RiskOccupantLevel> OccupantLevels { get; set; }
        public RiskSyncMetadata RiskSyncMetadata { get; set; }

    }
}
