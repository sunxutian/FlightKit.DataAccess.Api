using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskReport
    {
        public RiskReport()
        {
            AdditionDates = new HashSet<RiskAdditionDate>();
            Comments = new HashSet<RiskComment>();
            Exposures = new HashSet<RiskExposure>();
            FireDivisionRisks = new HashSet<RiskFireDivisionRisk>();
            FloorsAndRoofs = new HashSet<RiskFloorsAndRoof>();
            InternalProtections = new HashSet<RiskInternalProtection>();
            Occupants = new HashSet<RiskOccupant>();
            ProtectionSafeguards = new HashSet<RiskProtectionSafeguard>();
            ReportAddresses = new HashSet<RiskReportAddress>();
            ReportAttachments = new HashSet<RiskReportAttachment>();
            ReportBuildingInformations = new HashSet<RiskReportBuildingInformation>();
            ReportHazards = new HashSet<RiskReportHazard>();
            ReportPhotoes = new HashSet<RiskReportPhoto>();
            ReportRelatedDates = new HashSet<RiskReportRelatedDate>();
            RetiredOccupantNumbers = new HashSet<RiskRetiredOccupantNumber>();
            SecondaryConstructions = new HashSet<RiskSecondaryConstruction>();
            Walls = new HashSet<RiskWall>();
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
        public string DeleteComment { get; set; }
        public bool? HasInaccessibleArea { get; set; }
        public string InaccessibleAreaComment { get; set; }
        public bool? IsSprinklerNotUpdated { get; set; }
        public bool? IsWindNotUpdated { get; set; }

        public ICollection<RiskAdditionDate> AdditionDates { get; set; }
        public ICollection<RiskComment> Comments { get; set; }
        public ICollection<RiskExposure> Exposures { get; set; }
        public ICollection<RiskFireDivisionRisk> FireDivisionRisks { get; set; }
        public ICollection<RiskFloorsAndRoof> FloorsAndRoofs { get; set; }
        public ICollection<RiskInternalProtection> InternalProtections { get; set; }
        public ICollection<RiskOccupant> Occupants { get; set; }
        public ICollection<RiskProtectionSafeguard> ProtectionSafeguards { get; set; }
        public ICollection<RiskReportAddress> ReportAddresses { get; set; }
        public ICollection<RiskReportAttachment> ReportAttachments { get; set; }
        public ICollection<RiskReportBuildingInformation> ReportBuildingInformations { get; set; }
        public ICollection<RiskReportHazard> ReportHazards { get; set; }
        public ICollection<RiskReportPhoto> ReportPhotoes { get; set; }
        public ICollection<RiskReportRelatedDate> ReportRelatedDates { get; set; }
        public ICollection<RiskRetiredOccupantNumber> RetiredOccupantNumbers { get; set; }
        public ICollection<RiskSecondaryConstruction> SecondaryConstructions { get; set; }
        public ICollection<RiskWall> Walls { get; set; }
    }
}
