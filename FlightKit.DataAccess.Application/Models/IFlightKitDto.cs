using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Application.Models
{
    public interface IFlightKitDto
    {
        
    }

    public interface IFlightDtoWithReportId : IFlightKitDto
    {
        Guid ReportIdentifier { get; set; }
    }

    public interface IDtoWithSyncMetadata<TSyncMetadata> : IFlightKitDto
    where TSyncMetadata : ISyncMetadataDto
    {
        TSyncMetadata RiskSyncMetadata { get; set; }
    }

    public abstract class RiskDtoWithSyncMetadata : IDtoWithSyncMetadata<RiskSyncMetadata>
    {
        public RiskSyncMetadata RiskSyncMetadata { get; set; }
    }
}
