using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskCommentSegment : IFlightKitDto, IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public Guid CommentSegmentIdentifier { get; set; }
        public Guid CommentIdentifier { get; set; }
        public short CommentSegmentSequenceNumber { get; set; }
        public string CommentSegmentText { get; set; }
        public RiskSyncMetadata RiskSyncMetadata { get; set; }

    }
}
