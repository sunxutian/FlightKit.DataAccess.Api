﻿using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskProtectionSafeguardCode : IFlightKitDto
    {
        public string ProtectionSafeguardCodeValue { get; set; }
        public string ProtectionSafeguardCodeDescription { get; set; }
    }
}
