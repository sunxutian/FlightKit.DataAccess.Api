using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "FloorsAndRoofs")]
    public partial class Risk_FloorsAndRoof : RiskEntityWithSyncMetadata, IFlightKitEntityWithReportId
    {
        public Guid FloorAndRoofIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string LevelType { get; set; }
        public short? LevelStart { get; set; }
        public short? LevelStop { get; set; }
        public int? Width { get; set; }
        public int? Length { get; set; }
        public string ConstructionType { get; set; }
        public short? Damageability { get; set; }
        public string FireResistanceListing { get; set; }
        public short? HourlyRating { get; set; }
        public string Bgiitype { get; set; }
        public bool? IsVerticalOpeningProtection { get; set; }
        public bool? HasDividingWall { get; set; }
        public string DeckConstruction { get; set; }
        public string SupportType { get; set; }
        public bool? IsCombustibleInsulation { get; set; }
        public bool? IsFireClassified { get; set; }
        public bool? IsFinishSmokeDevelopmentAbove200 { get; set; }
        public short? MasonryThickness { get; set; }
        public string CombustibleType { get; set; }
        public bool? IsWoodIronClad { get; set; }
        public bool? IsHeavyTimberColumns { get; set; }
        public bool? IsExtraHeavyTimberColumns { get; set; }
        public bool? HasFoamPlasticInsulation { get; set; }
        public int? InsulationFlameSpread { get; set; }
        public int? InsulationSmokeDevelopment { get; set; }
        public bool? IsInsulationUnlisted { get; set; }
        public bool? HasAcceptableThermalBarrier { get; set; }
        public bool? IsInsulationSlowburning { get; set; }
        public bool? HasFireRetardantPaint { get; set; }
        public int? PaintFlameSpread { get; set; }
        public int? PaintSmokeDevelopment { get; set; }
        public bool? IsLowestLevel { get; set; }
        public bool? IsLowestLevelCombustible { get; set; }
        public bool IsAreaCalc { get; set; }
        public short? YearOfRoofCover { get; set; }
        public bool? IsYearOfRoofCoverApproximate { get; set; }

        public Risk_Report Report { get; set; }
    }
}
