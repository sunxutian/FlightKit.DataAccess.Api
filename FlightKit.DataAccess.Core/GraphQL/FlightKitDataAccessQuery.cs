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
            SyncMetadataFieldAsync<Risk_AdditionDate, RiskAdditionDate, RiskAdditionDateType>(r => r.AdditionDates);
            SyncMetadataFieldAsync<Risk_Comment, RiskComment, RiskCommentType>(r => r.Comments);
            SyncMetadataFieldAsync<Risk_CommentSegment, RiskCommentSegment, RiskCommentSegmentType>(r => r.Comments.SelectMany(c => c.CommentSegments));
            SyncMetadataFieldAsync<Risk_Exposure, RiskExposure, RiskExposureType>(r => r.Exposures);
            SyncMetadataFieldAsync<Risk_FireDivisionRisk, RiskFireDivisionRisk, RiskFireDivisionRiskType>(r => r.FireDivisionRisks);
            SyncMetadataFieldAsync<Risk_FloorsAndRoof, RiskFloorsAndRoof, RiskFloorsAndRoofType>(r => r.FloorsAndRoofs);
            SyncMetadataFieldAsync<Risk_InternalProtection, RiskInternalProtection, RiskInternalProtectionType>(r => r.InternalProtections);
            SyncMetadataFieldAsync<Risk_Occupant, RiskOccupant, RiskOccupantType>(r => r.Occupants);
            SyncMetadataFieldAsync<Risk_OccupantLevel, RiskOccupantLevel, RiskOccupantLevelType>(r => r.Occupants.SelectMany(o => o.OccupantLevels));
            SyncMetadataFieldAsync<Risk_OccupantHazard, RiskOccupantHazard, RiskOccupantHazardType>(r => r.Occupants.SelectMany(o => o.OccupantHazards));
            SyncMetadataFieldAsync<Risk_ProtectionSafeguard, RiskProtectionSafeguard, RiskProtectionSafeguardType>(r => r.ProtectionSafeguards);
            SyncMetadataFieldAsync<Risk_ReportAddress, RiskReportAddress, RiskReportAddressType>(r => r.ReportAddresses);
            SyncMetadataFieldAsync<Risk_ReportAttachment, RiskReportAttachment, RiskReportAttachmentType>(r => r.ReportAttachments);
            SyncMetadataFieldAsync<Risk_ReportBuildingInformation, RiskReportBuildingInformation, RiskReportBuildingInformationType>(r => r.ReportBuildingInformations);
            SyncMetadataFieldAsync<Risk_ReportHazard, RiskReportHazard, RiskReportHazardType>(r => r.ReportHazards);
            SyncMetadataFieldAsync<Risk_ReportPhoto, RiskReportPhoto, RiskReportPhotoType>(r => r.ReportPhotoes);
            SyncMetadataFieldAsync<Risk_ReportRelatedDate, RiskReportRelatedDate, RiskReportRelatedDateType>(r => r.ReportRelatedDates);
            SyncMetadataFieldAsync<Risk_RetiredOccupantNumber, RiskRetiredOccupantNumber, RiskRetiredOccupantNumberType>(r => r.RetiredOccupantNumbers);
            SyncMetadataFieldAsync<Risk_SecondaryConstruction, RiskSecondaryConstruction, RiskSecondaryConstructionType>(r => r.SecondaryConstructions);
            SyncMetadataFieldAsync<Risk_Wall, RiskWall, RiskWallType>(r => r.Walls);
        }

        #region private methods
        private void SyncMetadataFieldAsync<TEntity, TDto, TRiskDtoWithSyncMetadataType>(Expression<Func<Risk_Report, TEntity>> getDataExp)
            where TEntity : RiskEntityWithSyncMetadata, new()
            where TDto : RiskDtoWithSyncMetadata
            where TRiskDtoWithSyncMetadataType : GenericGraphQLType<TDto>
        {
            FieldAsync<ListGraphType<TRiskDtoWithSyncMetadataType>>($"{typeof(TDto).Name.ToCamelCase()}WithSyncMetadataByReportId",
                arguments:
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "reportId", Description = "Report Identifier" },
                    new QueryArgument<DateGraphType> { Name = "lastSyncDateTime", Description = "Last Sync Date Time" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("reportId");
                    var lastSyncTime = context.GetArgument<DateTime?>("lastSyncDateTime");

                    var result = await _reportDataServiceFactory()
                        .GetRiskDataWithSyncMetadataAsync<TEntity, TDto>(r => r.ReportIdentifier == id, getDataExp, lastSyncTime)
                        .ConfigureAwait(false);

                    return result;
                });

            FieldAsync<ListGraphType<TRiskDtoWithSyncMetadataType>>($"{typeof(TDto).Name.ToCamelCase()}WithSyncMetadataByOrderIds",
                arguments:
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<IntGraphType>>> { Name = "orderIds", Description = "OrderIds" },
                    new QueryArgument<DateGraphType> { Name = "lastSyncDateTime", Description = "Last Sync Date Time" }),
                resolve: async context =>
                {
                    var orderIds = context.GetArgument<List<long>>("orderIds");
                    var lastSyncTime = context.GetArgument<DateTime?>("lastSyncDateTime");

                    var result = await _reportDataServiceFactory()
                        .GetRiskDataWithSyncMetadataAsync<TEntity, TDto>(r => r.OrderId != null && 
                            orderIds.Contains(r.OrderId.Value), getDataExp, lastSyncTime)
                        .ConfigureAwait(false);

                    return result;
                });
        }

        private void SyncMetadataFieldAsync<TEntity, TDto, TRiskDtoWithSyncMetadataType>(Expression<Func<Risk_Report, IEnumerable<TEntity>>> getDataExp)
            where TEntity : RiskEntityWithSyncMetadata, new()
            where TDto : RiskDtoWithSyncMetadata
            where TRiskDtoWithSyncMetadataType : GenericGraphQLType<TDto>
        {
            FieldAsync<ListGraphType<TRiskDtoWithSyncMetadataType>>($"{typeof(TDto).Name.ToCamelCase()}WithSyncMetadataByReportId",
                arguments:
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "reportId", Description = "Report Identifier" },
                    new QueryArgument<DateGraphType> { Name = "lastSyncDateTime", Description = "Last Sync Date Time" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("reportId");
                    var lastSyncTime = context.GetArgument<DateTime?>("lastSyncDateTime");

                    var result = await _reportDataServiceFactory()
                        .GetRiskDataWithSyncMetadataAsync<TEntity, TDto>(r => r.ReportIdentifier == id, getDataExp, lastSyncTime)
                        .ConfigureAwait(false);

                    return result;
                });

            FieldAsync<ListGraphType<TRiskDtoWithSyncMetadataType>>($"{typeof(TDto).Name.ToCamelCase()}WithSyncMetadataByOrderIds",
                arguments:
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<IntGraphType>>> { Name = "orderIds", Description = "OrderIds" },
                    new QueryArgument<DateGraphType> { Name = "lastSyncDateTime", Description = "Last Sync Date Time" }),
                resolve: async context =>
                {
                    var orderIds = context.GetArgument<List<long>>("orderIds");
                    var lastSyncTime = context.GetArgument<DateTime?>("lastSyncDateTime");

                    var result = await _reportDataServiceFactory()
                        .GetRiskDataWithSyncMetadataAsync<TEntity, TDto>(r => r.OrderId != null && 
                            orderIds.Contains(r.OrderId.Value), getDataExp, lastSyncTime)
                        .ConfigureAwait(false);

                    return result;
                });
        }
        #endregion
    }
}
