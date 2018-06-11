using FlightKit.DataAccess.Domain.Data.Entity;
using FlightKit.DataAccess.Domain.Helpers;
using System;
using System.Reflection;

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

    public abstract class RiskEntityWithSyncMetadata : IEntityWithSyncMetadata<Risk_SyncMetadata>
    {
        public Risk_SyncMetadata RiskSyncMetadata { get; set; }
    }
}
