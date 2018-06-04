using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    public partial class Risk_ProtectionSafeguardCode : IFlightKitEntity
    {
        public string ProtectionSafeguardCodeValue { get; set; }
        public string ProtectionSafeguardCodeDescription { get; set; }
    }
}
