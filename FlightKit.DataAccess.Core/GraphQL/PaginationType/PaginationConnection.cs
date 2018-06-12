using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.GraphQL.PaginationType
{
    public class PaginationConnection<T>
    {
        public int TotalCount { get; set; }
        public ICollection<PaginationEdges<T>> Edges { get; set; }
        public PaginationPageInfo PageInfo { get; set; }
    }
}
