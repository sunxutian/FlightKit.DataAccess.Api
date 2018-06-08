using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Data
{
    public interface IServerSyncMetadata
    {
        Guid CorrelationId { get; set; }
        string SyncTable { get; set; }
        Guid? GuidId { get; set; }
        int? BigIntId { get; set; }
        string StringId { get; set; }
        bool IsCompositeKey { get; set; }
        int VersionNumber { get; set; }
        DateTime CreateUtcDateTime { get; set; }
        DateTime LastUpdateUtcDateTime { get; set; }
        bool IsDeleted { get; set; }
    }
}
