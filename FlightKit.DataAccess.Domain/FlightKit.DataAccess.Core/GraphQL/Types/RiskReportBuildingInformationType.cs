﻿using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskReportBuildingInformationType : GenericGraphQLType<RiskReportBuildingInformation>
    {
        public RiskReportBuildingInformationType() : base()
        {
        }
    }
}
