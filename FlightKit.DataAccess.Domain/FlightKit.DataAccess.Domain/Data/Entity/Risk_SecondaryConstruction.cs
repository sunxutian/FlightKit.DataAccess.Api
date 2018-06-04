using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_SecondaryConstruction : IFlightKitEntity
    {
        public Guid SecondaryConstructionIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short? PercentOfOtherFloors { get; set; }
        public short? NumberOfOtherFloors { get; set; }
        public bool? IsHeightMultiplierDoesNotApplied { get; set; }
        public int? LargestFloorArea { get; set; }
        public int? OtherFloorArea { get; set; }
        public string RoofSurfaceConstructionType { get; set; }
        public bool? IsFabricatedRoofFlameSpreadLessThan25 { get; set; }
        public int? RoofSurfaceArea { get; set; }
        public int? ConcealedRoofArea { get; set; }
        public int? InchesOfAirSpace { get; set; }
        public int? RaisedSurfaceFloorArea { get; set; }
        public decimal? PercentOfExteriorWallArea { get; set; }
        public string FinishInteriorCombustibleType { get; set; }
        public short? FinishInteriorPercentOfStoriesAffected { get; set; }
        public int? FinishInteriorFlameSpread { get; set; }
        public int? FinishInteriorSmokeDevelopment { get; set; }
        public bool? IsFinishInteriorUnlisted { get; set; }
        public bool? IsFinishInteriorThermalBarrierPresent { get; set; }
        public bool? IsFinishInteriorQualifiedAsSlowBurning { get; set; }
        public string FinishExteriorCombustibleType { get; set; }
        public short? FinishExteriorPercentOfStoriesAffected { get; set; }
        public int? FinishExteriorFlameSpread { get; set; }
        public int? FinishExteriorSmokeDevelopment { get; set; }
        public bool? IsFinishExteriorUnlisted { get; set; }
        public bool? IsFinishExteriorThermalBarrierPresent { get; set; }
        public bool? IsFinishExteriorQualifiedAsSlowBurning { get; set; }
        public string CombustibleExteriorAttachmentType { get; set; }
        public bool? IsCombustibleExteriorAttachmentSprinklered { get; set; }
        public int? CombustibleExteriorAttachmentArea { get; set; }
        public string BuildingConditionType { get; set; }
        public string BuildingConditionComment { get; set; }
        public string SolarPanelsCoverage { get; set; }
        public string GreenRoofCoverage { get; set; }
        public int? TotalEffectiveArea { get; set; }

        public Risk_Report Report { get; set; }
    }
}
