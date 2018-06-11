using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "Occupants")]
    public partial class Risk_Occupant : RiskEntityWithSyncMetadata, IFlightKitEntityWithReportId
    {
        public Risk_Occupant()
        {
            OccupantHazards = new HashSet<Risk_OccupantHazard>();
            OccupantLevels = new HashSet<Risk_OccupantLevel>();
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

        public Risk_Report Report { get; set; }
        public ICollection<Risk_OccupantHazard> OccupantHazards { get; set; }
        public ICollection<Risk_OccupantLevel> OccupantLevels { get; set; }
    }
}
