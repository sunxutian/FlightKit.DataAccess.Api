using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Domain.Data.Entity;
using FlightKit.DataAccess.Domain.Repo;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskCommentType : GenericGraphQLType<RiskComment>
    {
        public RiskCommentType() : base()
        {
            Field(r => r.CommentSegments, type: typeof(ListGraphType<RiskCommentSegmentType>));
            Field<RiskSyncMetadataType>("syncMetadata",
                resolve: c => c.Source.RiskSyncMetadata,
                description: "sync metadata");
            Interface<FlightKitDtoWithSyncMetadataType>();
        }
    }
}
