using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "ReportAddresses")]
    public partial class Risk_ReportAddress : IFlightKitEntityWithReportId, IEntityWithSyncMetadata<Risk_SyncMetadata>
    {
        public Guid ReportAddressIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public short AddressSequence { get; set; }
        public string LowNumber { get; set; }
        public string HighNumber { get; set; }
        public string Prefix { get; set; }
        public string PreDirection { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string PostDirection { get; set; }
        public string City { get; set; }
        public string PostalCity { get; set; }
        public string StateCode { get; set; }
        public string Zip { get; set; }
        public string Zip4 { get; set; }
        public string County { get; set; }
        public string AddressVerificationTypeCodeValue { get; set; }
        public string FireProtectionArea { get; set; }
        public string CommunityName { get; set; }
        public bool IsAlias { get; set; }

        public Risk_Report Report { get; set; }
        public Risk_SyncMetadata RiskSyncMetadata { get; set; }

    }
}
