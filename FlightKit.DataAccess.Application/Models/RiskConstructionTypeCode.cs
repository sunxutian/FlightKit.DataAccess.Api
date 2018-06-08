using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskConstructionTypeCode : IFlightKitDto
    {
        public string ConstructionTypeCodeValue { get; set; }
        public string ConstructionTypeCodeDescription { get; set; }
    }
}
