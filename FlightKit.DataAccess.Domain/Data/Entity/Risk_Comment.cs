using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "Comments")]
    public partial class Risk_Comment : IFlightKitEntityWithReportId, IEntityWithSyncMetadata<Risk_SyncMetadata>
    {
        public Risk_Comment()
        {
            CommentSegments = new HashSet<Risk_CommentSegment>();
        }

        public Guid CommentIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string CommentBy { get; set; }
        public DateTime? CommentDateTime { get; set; }
        public string CommentType { get; set; }

        public Risk_Report Report { get; set; }
        public ICollection<Risk_CommentSegment> CommentSegments { get; set; }
        public Risk_SyncMetadata RiskSyncMetadata { get; set; }

    }
}
