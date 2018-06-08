using System;
using System.Collections.Generic;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "ReportDateTypeCodes")]
    public partial class Risk_ReportDateTypeCode : IFlightKitEntity
    {
        public Risk_ReportDateTypeCode()
        {
            ReportRelatedDates = new HashSet<Risk_ReportRelatedDate>();
        }

        public string ReportDateTypeCodeValue { get; set; }
        public string ReportDateTypeCodeDescription { get; set; }

        public ICollection<Risk_ReportRelatedDate> ReportRelatedDates { get; set; }
    }
}
