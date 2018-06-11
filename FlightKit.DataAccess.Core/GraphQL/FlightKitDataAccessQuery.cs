using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.GraphQL.Types;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Domain.Data.Entity;
using FlightKit.DataAccess.Domain.Repo;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.GraphQL
{
    public class FlightKitDataAccessQuery : ObjectGraphType<object>
    {
        private static List<Type> _allSyncMetadataDtoType = typeof(RiskReport).Assembly
            .ExportedTypes.Where(t => !t.IsAbstract && t.IsClass)
            .Where(t =>
            {
                var inter = t.GetInterfaces().FirstOrDefault(i => i.IsConstructedGenericType);
                return inter?.GetGenericArguments()[0] == typeof(RiskSyncMetadata);
            }).ToList();

        public FlightKitDataAccessQuery(Func<IFlightKitReportDataService> reportDataService)
        {
            Name = "Query";

            FieldAsync<RiskReportType>("riskReportByReportId",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "reportId", Description = "Report Identifier" }),
                resolve: async context =>
                    {
                        var id = context.GetArgument<Guid>("reportId");
                        var report = await reportDataService().GetRiskReportByReportIdAsync(id, false).ConfigureAwait(false);
                        return report;
                    });

            FieldAsync<ListGraphType<RiskReportType>>("riskReportsByRiskId",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "riskId", Description = "Risk Id" }),
                resolve: async context =>
                {
                    var riskId = context.GetArgument<string>("riskId");
                    var reports = await reportDataService().GetRiskReportsByRiskIdAsync(riskId, false).ConfigureAwait(false);
                    return reports;
                });

            FieldAsync<ListGraphType<FlightKitDtoWithSyncMetadataType>>("riskDataWithSyncMetadataByReportId",
                arguments:
                new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "reportId", Description = "Report Identifier" },
                    new QueryArgument<DateGraphType> { Name = "lastSyncDateTime", Description = "Last Sync Date Time" },
                    new QueryArgument<StringGraphType> { Name = "tableName", Description = "data table name" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("reportId");
                    var lastSyncTime = context.GetArgument<DateTime>("lastSyncDateTime");
                    var tableName = (context.GetArgument<string>("tableName")).ToPascalCase();
                    Type dtoType = _allSyncMetadataDtoType.FirstOrDefault(t => t.Name.Equals(tableName, 
                        StringComparison.CurrentCultureIgnoreCase));
                    if (dtoType == null)
                    {
                        return null;
                    }

                    MethodInfo m = typeof(IFlightKitReportDataService)
                        .GetMethod(nameof(IFlightKitReportDataService.GetRiskDataWithSyncMetadataByReportIdAsync));

                    m = m.MakeGenericMethod(dtoType);

                    var interfaceResult = await (Task<ICollection<FlightKitDtoWithSyncMetadataType>>)(m.Invoke(reportDataService(), new object[]{ id }));

                    var result = interfaceResult.Select(v => Convert.ChangeType(v, dtoType));

                    return result;
                });
        }
    }
}
