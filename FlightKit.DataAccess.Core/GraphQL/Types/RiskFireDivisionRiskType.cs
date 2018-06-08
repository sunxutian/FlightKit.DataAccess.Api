using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskFireDivisionRiskType : GenericGraphQLType<RiskFireDivisionRisk>
    {
        public RiskFireDivisionRiskType() : base()
        {
            Field<RiskSyncMetadataType>("syncMetadata", resolve: context => context.Source.RiskSyncMetadata);
        }
    }
}
