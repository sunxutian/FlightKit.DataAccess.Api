using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskOccupantLevelType : GenericGraphQLType<RiskOccupantLevel>
    {
        public RiskOccupantLevelType() : base()
        {
            Field<RiskSyncMetadataType>("syncMetadata",
                resolve: c => c.Source.RiskSyncMetadata,
                description: "sync metadata");
            Interface<FlightKitDtoWithSyncMetadataType>();
        }
    }
}
