﻿using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "ReportAttachments")]
    public partial class Risk_ReportAttachment : RiskEntityWithSyncMetadata, IFlightKitEntityWithReportId
    {
        public Guid ReportAttachmentIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string AttachmentTypeCodeValue { get; set; }
        public short AttachmentSequence { get; set; }
        public string AttachmentDescription { get; set; }
        public DateTime AttachmentTimestamp { get; set; }
        public byte[] Attachment { get; set; }
        public string FileName { get; set; }

        public Risk_Report Report { get; set; }
    }
}
