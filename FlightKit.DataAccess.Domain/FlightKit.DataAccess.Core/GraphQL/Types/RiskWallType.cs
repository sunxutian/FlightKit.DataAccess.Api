﻿using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskWallType : GenericGraphQLType<RiskWall>
    {
        public RiskWallType() : base()
        {
            Field(r => r.ConstructionTypeCode, type: typeof(RiskConstructionTypeCodeType));
        }
    }
}