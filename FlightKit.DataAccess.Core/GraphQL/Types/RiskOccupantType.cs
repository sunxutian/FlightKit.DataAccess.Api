using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;
using GraphQL.Types;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskOccupantType : GenericGraphQLType<RiskOccupant>
    {
        public RiskOccupantType() : base()
        {
            Field(o => o.OccupantHazards, type: typeof(ListGraphType<RiskOccupantHazardType>));
            Field(o => o.OccupantLevels, type: typeof(ListGraphType<RiskOccupantLevelType>));
            Field<RiskSyncMetadataType>("syncMetadata", resolve: context => context.Source.RiskSyncMetadata);
        }
    }
}
