using System.Collections.Generic;

namespace FlightKit.DataAccess.Core.GraphQL.PaginationType
{
    public class PaginationEdges<T>
    {
        public T Node { get; set; }
        public string Cursor { get; set; }
    }
}
