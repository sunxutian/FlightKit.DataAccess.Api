using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Helpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HasLocationDataAttribute : Attribute
    {
        public HasLocationDataAttribute(string locationColumnName, string locationLongPropertyName, string locationLatPropertyName)
        {
            LocationColumnName = locationColumnName;
            LocationLongPropertyName = locationLongPropertyName;
            LocationLatPropertyName = locationLatPropertyName;
        }
        public string LocationColumnName { get; }
        public string LocationLongPropertyName { get; }
        public string LocationLatPropertyName { get; }
    }
}
