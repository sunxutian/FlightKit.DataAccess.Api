using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.GraphQL.Types;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Domain.Data.Entity;
using FlightKit.DataAccess.Domain.Repo;
using GraphQL.Types;
using System;
using System.Linq;

namespace FlightKit.DataAccess.Core.GraphQL
{
    public class FlightKitDataAccessQuery : ObjectGraphType<object>
    {
        public FlightKitDataAccessQuery(Func<IFlightKitReportDataService> reportDataService)
        {
            Name = "Query";

            FieldAsync<RiskReportType>("riskReportByReportId",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "reportId", Description = "Report Identifier" }),
                resolve: async context =>
                    {
                        var id = context.GetArgument<Guid>("reportId");
                        var report = await reportDataService().GetRiskReportByReportId(id).ConfigureAwait(false);
                        return report;
                    });
            FieldAsync<ListGraphType<RiskReportType>>("riskReportsByRiskId",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "riskId", Description = "Risk Id" }),
                resolve: async context =>
                {
                    var riskId = context.GetArgument<string>("riskId");
                    var reports = await reportDataService().GetRiskReportsByRiskId(riskId).ConfigureAwait(false);
                    return reports;
                });
        }
    }
}
