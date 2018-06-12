using GraphQL.Types;

namespace FlightKit.DataAccess.Core.GraphQL.PaginationType
{
    public class PaginationEdgesType<TGraphType, T> : ObjectGraphType<PaginationEdges<T>>
        where TGraphType : ObjectGraphType<T>
    {
        public PaginationEdgesType()
        {
            Name = $"{typeof(T).Name}PageNationEdgesType";
            Field(p => p.Cursor);
            Field<TGraphType>("node", resolve: context => context.Source.Node);
        }
    }

    public class PaginationPageInfoType : ObjectGraphType<PaginationPageInfo>
    {
        public PaginationPageInfoType()
        {
            Field(i => i.EndCursor);
            Field(i => i.HasNextPage);
        }
    }

    public class PaginationConnectionType<TGraphType, T> : ObjectGraphType<PaginationConnection<T>>
        where TGraphType : ObjectGraphType<T>
    {
        public PaginationConnectionType()
        {
            Name = $"{typeof(T).Name}PaginationConnectionType";
            Field<IntGraphType>(name: "reportsTotalCount",
                description: "number of reports which has this data and meets the searching criteria",
                resolve: context => context.Source.TotalCount);
            Field<ListGraphType<PaginationEdgesType<TGraphType, T>>>("edges", resolve: context => context.Source.Edges);
            Field<PaginationPageInfoType>("pageInfo", resolve: context => context.Source.PageInfo);
        }
    }
}
