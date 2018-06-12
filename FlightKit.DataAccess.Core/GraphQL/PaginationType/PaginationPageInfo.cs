namespace FlightKit.DataAccess.Core.GraphQL.PaginationType
{
    public class PaginationPageInfo
    {
        public string EndCursor { get; set; }
        public bool HasNextPage { get; set; }
    }
}
