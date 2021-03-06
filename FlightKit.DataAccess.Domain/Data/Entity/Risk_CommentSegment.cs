﻿using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "CommentSegments")]
    public partial class Risk_CommentSegment : RiskEntityWithSyncMetadata, IFlightKitEntity
    {
        public Guid CommentSegmentIdentifier { get; set; }
        public Guid CommentIdentifier { get; set; }
        public short CommentSegmentSequenceNumber { get; set; }
        public string CommentSegmentText { get; set; }

        public Risk_Comment Comment { get; set; }
    }
}
