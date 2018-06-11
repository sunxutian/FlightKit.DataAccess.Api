using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.GraphQL.Types;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Domain.Data;
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
        private readonly Func<IFlightKitReportDataService> _reportDataServiceFactory;

        public FlightKitDataAccessQuery(Func<IFlightKitReportDataService> reportDataServiceFactory)
        {
            Name = "Query";
            _reportDataServiceFactory = reportDataServiceFactory;

            FieldAsync<RiskReportType>("riskReportByReportId",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "reportId", Description = "Report Identifier" }),
                resolve: async context =>
                    {
                        var id = context.GetArgument<Guid>("reportId");
                        var report = await reportDataServiceFactory().GetRiskReportByReportIdAsync(id, false).ConfigureAwait(false);
                        return report;
                    });

            FieldAsync<ListGraphType<RiskReportType>>("riskReportsByRiskId",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "riskId", Description = "Risk Id" }),
                resolve: async context =>
                {
                    var riskId = context.GetArgument<string>("riskId");
                    var reports = await reportDataServiceFactory().GetRiskReportsByRiskIdAsync(riskId, false).ConfigureAwait(false);
                    return reports;
                });

            SyncMetadataFieldAsync<Risk_Report, RiskReport, RiskReportType>(r => r);
        }

        private FieldType SyncMetadataFieldAsync<TEntity, TDto, TRiskDtoWithSyncMetadataType>(Expression<Func<Risk_Report, TEntity>> getDataExp)
            where TEntity : class, IEntityWithSyncMetadata<Risk_SyncMetadata>, new()
            where TDto : RiskDtoWithSyncMetadata
            where TRiskDtoWithSyncMetadataType : IComplexGraphType
        {
            return FieldAsync<ListGraphType<TRiskDtoWithSyncMetadataType>>($"{typeof(TDto).Name.ToCamelCase()}WithSyncMetadataByReportId",
                arguments:
                new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "reportId", Description = "Report Identifier" },
                    new QueryArgument<DateGraphType> { Name = "lastSyncDateTime", Description = "Last Sync Date Time" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("reportId");
                    var lastSyncTime = context.GetArgument<DateTime>("lastSyncDateTime");

                    var result = await _reportDataServiceFactory()
                        .GetRiskDataWithSyncMetadataByReportIdAsync<TEntity, TDto>(r => r.ReportIdentifier == id, getDataExp)
                        .ConfigureAwait(false);

                    return result;
                });
        }
    }
}
