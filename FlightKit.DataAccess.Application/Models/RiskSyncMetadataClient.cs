using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Application.Models
{
    public class RiskSyncMetadataClient : IFlightKitDto
    {
        public Guid SyncMetadataClientId { get; set; }
        public Guid ClientId { get; set; }
        public string SyncTable { get; set; }
        public DateTime LastSyncUtcDateTime { get; set; }
    }
}
