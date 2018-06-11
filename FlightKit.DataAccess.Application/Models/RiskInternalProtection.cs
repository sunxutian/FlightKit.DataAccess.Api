using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskInternalProtection : RiskDtoWithSyncMetadata, IFlightDtoWithReportId
    {
        public Guid InternalProtectionIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public bool? HasNoInternalProtection { get; set; }
        public short? FireAlarmGrade { get; set; }
        public bool? IsAutoFireSelected { get; set; }
        public bool? HasWatchman { get; set; }
        public string WatchmanType { get; set; }
        public int? PartialSupplyProtectedArea { get; set; }
        public string PartialSupplyAlarmType { get; set; }
        public int? LimitedSupplyProtectedArea { get; set; }
        public string LimitedSupplyAlarmType { get; set; }
        public bool? IsStandardAutoSprinklerSelected { get; set; }
        public string SprinkleredStationType { get; set; }
        public string SprinkleredStationAlarmType { get; set; }
        public bool? HasStandardPeriodicInspectionService { get; set; }
        public bool? HasExtinguishers { get; set; }
        public bool? HasStandpipeHose { get; set; }
        public string AlarmSignalDestinationType { get; set; }
        public string TroubleSignalDestinationType { get; set; }
        public string AutoFireAlarmSystemCoverageType { get; set; }
        public string AutoFireAlarmInspectionAndTestFrequencyType { get; set; }
        public bool? IsSystemAndTransmitterListed { get; set; }
        public bool? IsRecieverListed { get; set; }
        public string Comment { get; set; }
        public bool HasNonStandardRetransmission { get; set; }
    }
}
