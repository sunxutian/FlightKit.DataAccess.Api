using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Helpers
{
    /// <summary>
    /// Indicate the handler will not modify db
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class NotTrackDbChangeAttribute : Attribute
    {

    }
}
