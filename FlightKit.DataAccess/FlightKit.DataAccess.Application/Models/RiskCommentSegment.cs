using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskCommentSegment
    {
        public Guid CommentSegmentIdentifier { get; set; }
        public Guid CommentIdentifier { get; set; }
        public short CommentSegmentSequenceNumber { get; set; }
        public string CommentSegmentText { get; set; }
    }
}
