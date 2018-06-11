using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.Helpers
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class QueryWithSyncMetadataAttribute : Attribute
    {
        
    }
}
