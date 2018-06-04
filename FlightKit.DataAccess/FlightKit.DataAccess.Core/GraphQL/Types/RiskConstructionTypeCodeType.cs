using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Domain.Data.Entity;
using FlightKit.DataAccess.Domain.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskConstructionTypeCodeType : GenericGraphQLType<RiskConstructionTypeCode>
    {
        public RiskConstructionTypeCodeType() : base()
        {
        }
    }
}
