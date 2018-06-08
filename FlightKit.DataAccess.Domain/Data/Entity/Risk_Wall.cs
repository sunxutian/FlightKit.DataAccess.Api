using FlightKit.DataAccess.Domain.Data.Entity;
using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_Wall : IFlightKitEntityWithReportId
    {
        public Guid WallIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short LineNumber { get; set; }
        public short? Thickness { get; set; }
        public short? WallClassification { get; set; }
        public bool? IsReinforcedConcrete { get; set; }
        public short? Damagibility { get; set; }
        public short? HourlyRating { get; set; }
        public bool? IsNoncombustible { get; set; }
        public bool? IsSlowBurning { get; set; }
        public bool? IsMasonryVeneer { get; set; }
        public short? InsulationFlameSpread { get; set; }
        public short? InsulationSmokeDevelopment { get; set; }
        public bool? IsUnlisted { get; set; }
        public string CombustibilityClass { get; set; }
        public bool? IsWoodIronClad { get; set; }
        public string FireResistiveListType { get; set; }
        public bool IsFlameSpreadAbove200 { get; set; }
        public bool HasAcceptableThermalBarrier { get; set; }
        public bool HasNote2 { get; set; }
        public int? PaintFlameSpread { get; set; }
        public int? PaintSmokeDevelopment { get; set; }
        public string ConstructionTypeCodeValue { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool? IsCombustible { get; set; }
        public string CombustibilityRating { get; set; }
        public bool? HasFoamPlasticInsulation { get; set; }
        public bool? HasFireRetardantPaint { get; set; }

        public Risk_ConstructionTypeCode ConstructionTypeCode { get; set; }
        public Risk_Report Report { get; set; }
    }
}
