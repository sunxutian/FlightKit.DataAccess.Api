using FlightKit.DataAccess.Domain.Data.Entity;
using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_Report : IFlightKitEntity
    {
        public Risk_Report()
        {
            AdditionDates = new HashSet<Risk_AdditionDate>();
            Comments = new HashSet<Risk_Comment>();
            Exposures = new HashSet<Risk_Exposure>();
            FireDivisionRisks = new HashSet<Risk_FireDivisionRisk>();
            FloorsAndRoofs = new HashSet<Risk_FloorsAndRoof>();
            InternalProtections = new HashSet<Risk_InternalProtection>();
            Occupants = new HashSet<Risk_Occupant>();
            ProtectionSafeguards = new HashSet<Risk_ProtectionSafeguard>();
            ReportAddresses = new HashSet<Risk_ReportAddress>();
            ReportAttachments = new HashSet<Risk_ReportAttachment>();
            ReportBuildingInformations = new HashSet<Risk_ReportBuildingInformation>();
            ReportHazards = new HashSet<Risk_ReportHazard>();
            ReportPhotoes = new HashSet<Risk_ReportPhoto>();
            ReportRelatedDates = new HashSet<Risk_ReportRelatedDate>();
            RetiredOccupantNumbers = new HashSet<Risk_RetiredOccupantNumber>();
            SecondaryConstructions = new HashSet<Risk_SecondaryConstruction>();
            Walls = new HashSet<Risk_Wall>();
        }

        public Guid ReportIdentifier { get; set; }
        public string RiskId { get; set; }
        public string BuildingDescription { get; set; }
        public string RiskTypeCodeValue { get; set; }
        public string SurveyStatusCodeValue { get; set; }
        public string BuildingFireRatedCodeValue { get; set; }
        public string BuildingFireConstructionCodeValue { get; set; }
        public string PublicProtectionClass { get; set; }
        public short? YearBuilt { get; set; }
        public string BuildingWindCommercialStatisticalPlanCodeValue { get; set; }
        public string BasicGroup2Symbol { get; set; }
        public string CommercialStatisticalPlanTerritoryCode { get; set; }
        public string CommercialStatisticalPlanCode { get; set; }
        public short? NumberOfFloors { get; set; }
        public short? SprinklerRatingAsgr { get; set; }
        public bool? IsYearBuiltApproximate { get; set; }
        public string SprinklerTypeCodeValue { get; set; }
        public string BuildingWindConstructionCodeValue { get; set; }
        public string ClmClassCsp { get; set; }
        public string ColumnType { get; set; }
        public string PanelConstructionType { get; set; }
        public string CombustibilityClass { get; set; }
        public string FinalScheduleResultType { get; set; }
        public int? FinalExposedBuildingGrade { get; set; }
        public decimal? PercentWallCombustible { get; set; }
        public decimal? PercentwallNoncombustible { get; set; }
        public decimal? PercentWallModeratelyFireResistive { get; set; }
        public decimal? PercentWallFireResistive { get; set; }
        public decimal? PercentFloorCombustible { get; set; }
        public decimal? PercentFloorFireResistive { get; set; }
        public decimal? PercentFloorHourlyRatingEqualsOne { get; set; }
        public decimal? PercentFloorHourlyRatingGreaterThanOne { get; set; }
        public int? NeededFireFlow { get; set; }
        public string TerritoryCode { get; set; }
        public string BillingType { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string EscortedByName { get; set; }
        public string EscortedByPhone { get; set; }
        public bool? HasMultipleFireDivisions { get; set; }
        public short? BasementLevels { get; set; }
        public short? ConstructionClass { get; set; }
        public bool? IsWindUplift90 { get; set; }
        public string SkylightsRecognition { get; set; }
        public bool? IsCc3 { get; set; }
        public bool? IsMixedLowHighRise { get; set; }
        public bool? Is22gaRoof { get; set; }
        public bool? IsSteelLight { get; set; }
        public bool? IsSteelOtherThanLight { get; set; }
        public long? OrderId { get; set; }
        public string FieldRep { get; set; }
        public string Status { get; set; }
        public bool? IsOpenSided { get; set; }
        public bool? IsReinforcedConcrete { get; set; }
        public bool? IsConstructionClassOverride { get; set; }
        public bool? IsBasicGroup2SymbolOverride { get; set; }
        public decimal? WaterSupplyWorksAdequacy { get; set; }
        public decimal? HydrantSpacingAdequacy { get; set; }
        public int? FireFlowAt20Psi { get; set; }
        public decimal? FireDepartmentCompaniesAdequacy { get; set; }
        public decimal? OverallAdequacy { get; set; }
        public bool? HasUnprotectedMetalColumns { get; set; }
        public short? PercentUnprotectedMetalColumnArea { get; set; }
        public bool? HasCombustibleColumns { get; set; }
        public short? PercentCombustibleColumnArea { get; set; }
        public bool? HasNonCombustiblePanels { get; set; }
        public short? PercentNonCombustiblePanelArea { get; set; }
        public bool? HasGlassPanels { get; set; }
        public short? PercentGlassPanelArea { get; set; }
        public bool? HasSlowBurnPanels { get; set; }
        public short? PercentSlowBurnPanelArea { get; set; }
        public bool? HasCombustiblePanels { get; set; }
        public short? PercentCombustiblePanelArea { get; set; }
        public short? RoofPercentLightFrameConstruction { get; set; }
        public bool? IsRoofPercentLightFrameConstructionNotAvailable { get; set; }
        public short? FloorPercentLightFrameConstruction { get; set; }
        public bool? IsFloorPercentLightFrameConstructionNotAvailable { get; set; }
        public short? ColumnWallThickness { get; set; }
        public bool? IsCc4 { get; set; }
        public string FileNumber { get; set; }
        public string DeleteReasonCode { get; set; }
        public bool? HasInaccessibleArea { get; set; }
        public string InaccessibleAreaComment { get; set; }
        public bool? IsSprinklerNotUpdated { get; set; }
        public bool? IsWindNotUpdated { get; set; }

        public ICollection<Risk_AdditionDate> AdditionDates { get; set; }
        public ICollection<Risk_Comment> Comments { get; set; }
        public ICollection<Risk_Exposure> Exposures { get; set; }
        public ICollection<Risk_FireDivisionRisk> FireDivisionRisks { get; set; }
        public ICollection<Risk_FloorsAndRoof> FloorsAndRoofs { get; set; }
        public ICollection<Risk_InternalProtection> InternalProtections { get; set; }
        public ICollection<Risk_Occupant> Occupants { get; set; }
        public ICollection<Risk_ProtectionSafeguard> ProtectionSafeguards { get; set; }
        public ICollection<Risk_ReportAddress> ReportAddresses { get; set; }
        public ICollection<Risk_ReportAttachment> ReportAttachments { get; set; }
        public ICollection<Risk_ReportBuildingInformation> ReportBuildingInformations { get; set; }
        public ICollection<Risk_ReportHazard> ReportHazards { get; set; }
        public ICollection<Risk_ReportPhoto> ReportPhotoes { get; set; }
        public ICollection<Risk_ReportRelatedDate> ReportRelatedDates { get; set; }
        public ICollection<Risk_RetiredOccupantNumber> RetiredOccupantNumbers { get; set; }
        public ICollection<Risk_SecondaryConstruction> SecondaryConstructions { get; set; }
        public ICollection<Risk_Wall> Walls { get; set; }
    }
}
