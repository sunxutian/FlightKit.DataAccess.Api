using FlightKit.DataAccess.Application.Models;
using GraphQL.Types;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class FlightKitDtoWithSyncMetadataType : InterfaceGraphType<RiskDtoWithSyncMetadata>
    {
        public FlightKitDtoWithSyncMetadataType()
        {
            Name = "RiskDataWithSyncMetadata";
            Field<RiskSyncMetadataType>("syncMetadata", description: "sync meta data of risk data");
        }
    }
}
