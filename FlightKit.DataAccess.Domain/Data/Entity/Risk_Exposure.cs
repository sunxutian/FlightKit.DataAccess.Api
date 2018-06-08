using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_Exposure : IFlightKitEntityWithReportId
    {
        public Guid ExposureIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string ExposureDirection { get; set; }
        public int? SurveyDistanceExposure { get; set; }
        public string RiskConstructionType { get; set; }
        public string RiskProtectionType { get; set; }
        public string ExposureConstructionType { get; set; }
        public string ExposureProtectionType { get; set; }
        public short? LengthHeightFactor { get; set; }
        public string OccupancyType { get; set; }
        public bool? HasOutsideAutomaticSprinkler { get; set; }
        public bool? HasCommunications { get; set; }
        public bool? IsPassage { get; set; }
        public string PartyProtectionType { get; set; }
        public string PassageCombustibleType { get; set; }
        public string PassageProtectionType { get; set; }
        public int? PassageLength { get; set; }
        public bool? IsPassageOpen { get; set; }
        public bool? IsPartyWall { get; set; }

        public Risk_Report Report { get; set; }
    }
}
