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
        public FlightKitDataAccessQuery(Func<IDbRepository<Risk_Report>> repoFactory, IMappingHelperService mapper)
        {
            Name = "Query";

            FieldAsync<RiskReportType>("riskReportByReportId",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "reportId", Description = "Report Identifier" }),
                resolve: async context =>
                    {
                        var id = context.GetArgument<Guid>("reportId");
                        var reports = await mapper.GetMappedDtoFromDbAsync<Risk_Report, RiskReport>(repoFactory(), r => r.ReportIdentifier == id);
                        return reports.FirstOrDefault();
                    });
            FieldAsync<ListGraphType<RiskReportType>>("riskReportsByRiskId",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "riskId", Description = "Risk Id" }),
                resolve: async context =>
                {
                    var riskId = context.GetArgument<string>("riskId");
                    return await mapper.GetMappedDtoFromDbAsync<Risk_Report, RiskReport>(repoFactory(), r => r.RiskId == riskId);
                });
        }
    }
}
