using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Application.Models
{
    public partial class RiskReportAttachment : RiskDtoWithSyncMetadata, IFlightDtoWithReportId
    {
        public Guid ReportAttachmentIdentifier { get; set; }
        public Guid ReportIdentifier { get; set; }
        public string AttachmentTypeCodeValue { get; set; }
        public short AttachmentSequence { get; set; }
        public string AttachmentDescription { get; set; }
        public DateTime AttachmentTimestamp { get; set; }
        public byte[] Attachment { get; set; }
        public string FileName { get; set; }
        public string AttachmentBase64 => Convert.ToBase64String(Attachment ?? new byte[] { });
    }
}
