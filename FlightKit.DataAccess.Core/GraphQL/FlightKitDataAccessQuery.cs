using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.GraphQL.PaginationType;
using FlightKit.DataAccess.Core.GraphQL.Types;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using FlightKit.DataAccess.Domain.Data;
using FlightKit.DataAccess.Domain.Data.Entity;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FlightKit.DataAccess.Core.GraphQL
{
    public class FlightKitDataAccessQuery : ObjectGraphType<object>
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public FlightKitDataAccessQuery(ICommandHandlerFactory commandHandlerFactory)
        {
            Name = "Query";
            _commandHandlerFactory = commandHandlerFactory;

            FieldAsync<RiskReportType>("riskReportByReportId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "reportId", Description = "Report Identifier" },
                    new QueryArgument<BooleanGraphType> { Name = "includeSyncMetadata", Description = "If returns the data with sync metadata", DefaultValue = false }),
                resolve: async context =>
                    {
                        var id = context.GetArgument<Guid>("reportId");
                        var includesSyncMetadata = context.GetArgument<bool>("includeSyncMetadata");
                        GetRiskReportByReportIdCommand command = new GetRiskReportByReportIdCommand
                        {
                            ReportId = id,
                            IncludeSyncMetadata = includesSyncMetadata
                        };

                        var report = await _commandHandlerFactory.
                            RequestCommandHandler<GetRiskReportByReportIdCommand, RiskReport>().HandleAsync(command)
                            .ConfigureAwait(false);

                        return report;
                    });

            FieldAsync<ListGraphType<RiskReportType>>("riskReportsByRiskId",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "riskId", Description = "Risk Id" },
                    new QueryArgument<BooleanGraphType> { Name = "includeSyncMetadata", Description = "If returns the data with sync metadata", DefaultValue = false }),
                resolve: async context =>
                {
                    var riskId = context.GetArgument<string>("riskId");
                    var includesSyncMetadata = context.GetArgument<bool>("includeSyncMetadata");
                    GetRiskReportsByRiskIdCommand command = new GetRiskReportsByRiskIdCommand
                    {
                        RiskId = riskId,
                        IncludeSyncMetadata = includesSyncMetadata
                    };

                    var reports = await _commandHandlerFactory
                    .RequestCommandHandler<GetRiskReportsByRiskIdCommand, ICollection<RiskReport>>()
                        .HandleAsync(command).ConfigureAwait(false);

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

                    GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto> command =
                        new GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>(
                            r => r.ReportIdentifier == id, getDataExp, lastSyncTime);

                    var result = await _commandHandlerFactory
                    .RequestCommandHandler<GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>,
                        (ICollection<TDto> data, int totalReportsCount, Guid? endReportIdCursor, bool hasNext)>()
                        .HandleAsync(command).ConfigureAwait(false);

                    return result.data;
                });

            FieldAsync<PaginationConnectionType<TRiskDtoWithSyncMetadataType, TDto>>($"{typeof(TDto).Name.ToCamelCase()}WithSyncMetadataByOrderIds",
                arguments:
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<IntGraphType>>> { Name = "orderIds", Description = "OrderIds" },
                    new QueryArgument<DateGraphType> { Name = "lastSyncDateTime", Description = "Last Sync Date Time" },
                    new QueryArgument<EnumerationGraphType<OrderBy>> { Name = "orderBy", DefaultValue = OrderBy.ReportId },
                    new QueryArgument<EnumerationGraphType<Order>> { Name = "order", DefaultValue = Order.Ascending },
                    new QueryArgument<IntGraphType> { Name = "first", Description = "only query first x rows of data" },
                    new QueryArgument<IdGraphType> { Name = "after", Description = "after this cursor to query data" }),
                resolve: async context =>
                {
                    var orderIds = context.GetArgument<List<long>>("orderIds");
                    var lastSyncTime = context.GetArgument<DateTime?>("lastSyncDateTime");
                    var first = context.GetArgument<int?>("first");
                    var after = context.GetArgument<Guid?>("after");
                    var orderBy = context.GetArgument<OrderBy>("orderBy");
                    var order = context.GetArgument<Order>("order");
                    bool isascending = order == Order.Ascending;
                    Expression<Func<Risk_Report, bool>> filter =
                        r => r.OrderId != null && orderIds.Contains(r.OrderId.Value);

                    Expression<Func<Risk_Report, IComparable>> orderby = 
                        first != null || after != null 
                        ? GetOrderByExpression(orderBy) 
                        : null;

                    GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto> command =
                        new GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>(
                            filter, getDataExp, lastSyncTime,
                            orderby, isascending, after, first);

                    (ICollection<TDto> data, int totalReportsCount, Guid? endReportCursor, bool hasNext) =
                        await _commandHandlerFactory.RequestCommandHandler<GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>,
                        (ICollection<TDto> data, int totalReportsCount, Guid? endReportCursor, bool hasNext)>()
                        .HandleAsync(command).ConfigureAwait(false);

                    return new PaginationConnection<TDto>
                    {
                        TotalCount = totalReportsCount,
                        Edges = data.Select(d => new PaginationEdges<TDto> { Node = d, Cursor = d.ReportId?.ToString() }).ToList(),
                        PageInfo = new PaginationPageInfo { EndCursor = endReportCursor?.ToString(), HasNextPage = hasNext }
                    };
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
                    GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto> command =
                       new GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>(
                           r => r.ReportIdentifier == id, getDataExp, lastSyncTime);

                    var result = await _commandHandlerFactory
                    .RequestCommandHandler<GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>,
                        (ICollection<TDto> data, int totalReportsCount, Guid? endReportIdCursor, bool hasNext)>()
                        .HandleAsync(command).ConfigureAwait(false);

                    return result.data;
                });

            FieldAsync<PaginationConnectionType<TRiskDtoWithSyncMetadataType, TDto>>($"{typeof(TDto).Name.ToCamelCase()}WithSyncMetadataByOrderIds",
                arguments:
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<ListGraphType<IntGraphType>>> { Name = "orderIds", Description = "OrderIds" },
                    new QueryArgument<DateGraphType> { Name = "lastSyncDateTime", Description = "Last Sync Date Time" },
                    new QueryArgument<EnumerationGraphType<OrderBy>> { Name = "orderBy", DefaultValue = OrderBy.ReportId },
                    new QueryArgument<EnumerationGraphType<Order>> { Name = "order", DefaultValue = Order.Ascending },
                    new QueryArgument<IntGraphType> { Name = "first", Description = "only query first x rows of data" },
                    new QueryArgument<IdGraphType> { Name = "after", Description = "after this cursor to query data" }),
                resolve: async context =>
                {
                    var orderIds = context.GetArgument<List<long>>("orderIds");
                    var lastSyncTime = context.GetArgument<DateTime?>("lastSyncDateTime");
                    var first = context.GetArgument<int?>("first");
                    var after = context.GetArgument<Guid?>("after");
                    var order = context.GetArgument<Order>("order");
                    var orderBy = context.GetArgument<OrderBy>("orderBy");
                    bool isascending = order == Order.Ascending;
                    Expression<Func<Risk_Report, bool>> filter =
                        r => r.OrderId != null && orderIds.Contains(r.OrderId.Value);

                    Expression<Func<Risk_Report, IComparable>> orderby =
                        first != null || after != null
                        ? GetOrderByExpression(orderBy)
                        : null;

                    GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto> command =
                        new GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>(
                            filter, getDataExp, lastSyncTime,
                            orderby, isascending, after, first);

                    (ICollection<TDto> data, int totalReportsCount, Guid? endReportCursor, bool hasNext) =
                        await _commandHandlerFactory.RequestCommandHandler<GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>,
                        (ICollection<TDto> data, int totalReportsCount, Guid? endReportCursor, bool hasNext)>()
                        .HandleAsync(command).ConfigureAwait(false);

                    return new PaginationConnection<TDto>
                    {
                        TotalCount = totalReportsCount,
                        Edges = data.Select(d => new PaginationEdges<TDto> { Node = d, Cursor = d.ReportId?.ToString() }).ToList(),
                        PageInfo = new PaginationPageInfo { EndCursor = endReportCursor?.ToString(), HasNextPage = hasNext }
                    };
                });
        }

        private Expression<Func<Risk_Report, IComparable>> GetOrderByExpression( OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.OrderId:
                    return r => r.OrderId;
                case OrderBy.ReportId:
                    return r => r.ReportIdentifier;
                case OrderBy.OnSiteSurveyDate:
                    return r => r.ReportRelatedDates
                        .FirstOrDefault(d => d.ReportDateTypeCodeValue == "OSSD")
                        .ReportDateTime;
                case OrderBy.ScheduleApplyDate:
                    return r => r.ReportRelatedDates
                        .FirstOrDefault(d => d.ReportDateTypeCodeValue == "SDAP")
                        .ReportDateTime;
                default:
                    return null;
            }
        }

        #endregion

        #region private helpers
        private enum Order
        {
            Ascending,
            Descending
        }

        private enum OrderBy
        {
            OrderId,
            ReportId,
            OnSiteSurveyDate,
            ScheduleApplyDate
        }
        #endregion
    }
}
