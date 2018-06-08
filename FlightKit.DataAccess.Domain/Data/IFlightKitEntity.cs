using FlightKit.DataAccess.Domain.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Data
{
    public interface IFlightKitEntity
    {
        
    }

    public interface IEntityWithSyncMetadata<TSyncMetadata> : IFlightKitEntity
        where TSyncMetadata : IServerSyncMetadata
    {
        TSyncMetadata RiskSyncMetadata { get; set; }
    }

    public interface IFlightKitEntityWithReportId : IFlightKitEntity
    {
        Guid ReportIdentifier { get; set; }
    }
}
