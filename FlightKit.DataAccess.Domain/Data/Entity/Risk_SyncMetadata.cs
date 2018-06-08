using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "SyncMetadata")]
    public class Risk_SyncMetadata : IFlightKitEntity, IServerSyncMetadata
    {
        public Guid CorrelationId { get; set; }
        public string SyncTable { get; set; }
        public Guid? GuidId { get; set; }
        public int? BigIntId { get; set; }
        public string StringId { get; set; }
        public bool IsCompositeKey { get; set; }
        public int VersionNumber { get; set; }
        public DateTime CreateUtcDateTime { get; set; }
        public DateTime LastUpdateUtcDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
