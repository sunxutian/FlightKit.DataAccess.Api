using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Data.Entity
{
    [Helpers.TableName("Risks", "SyncMetadataClient")]
    public class Risk_SyncMetadataClient
    {
        public Guid SyncMetadataClientId { get; set; }
        public Guid ClientId { get; set; }
        public string SyncTable { get; set; }
        public DateTime LastSyncUtcDateTime { get; set; }
    }
}
