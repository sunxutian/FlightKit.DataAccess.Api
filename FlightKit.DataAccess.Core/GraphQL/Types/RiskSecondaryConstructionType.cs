using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskSecondaryConstructionType : GenericGraphQLType<RiskSecondaryConstruction>
    {
        public RiskSecondaryConstructionType() : base()
        {
            Field<RiskSyncMetadataType>("syncMetadata", resolve: context => context.Source.RiskSyncMetadata);
        }
    }
}
