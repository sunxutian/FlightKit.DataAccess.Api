using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskComment
    {
        public RiskComment()
        {
            CommentSegments = new HashSet<RiskCommentSegment>();
        }

        public Guid CommentIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string CommentBy { get; set; }
        public DateTime? CommentDateTime { get; set; }
        public string CommentType { get; set; }
        public ICollection<RiskCommentSegment> CommentSegments { get; set; }
    }
}
