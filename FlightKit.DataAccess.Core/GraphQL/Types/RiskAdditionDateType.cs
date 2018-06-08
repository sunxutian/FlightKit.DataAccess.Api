using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;
using GraphQL.Types;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskAdditionDateType : GenericGraphQLType<RiskAdditionDate>
    {
        public RiskAdditionDateType() : base()
        {
            Field<RiskSyncMetadataType>("syncMetadata", resolve: context => context.Source.RiskSyncMetadata);
        }
    }
}
