using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Domain.Helpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; }
        public string SchemaName { get; }
        public TableNameAttribute(string schemaName, string tableName)
        {
            TableName = tableName;
            SchemaName = schemaName;
        }
    }
}
