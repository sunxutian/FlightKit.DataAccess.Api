﻿using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskAdditionDate : RiskDtoWithSyncMetadata, IFlightDtoWithReportId
    {
        public Guid AdditionDateIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public DateTime AdditionDate { get; set; }
    }
}
