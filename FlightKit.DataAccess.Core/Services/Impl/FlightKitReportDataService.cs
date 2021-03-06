﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data;
using FlightKit.DataAccess.Domain.Data.Entity;
using FlightKit.DataAccess.Domain.Helpers;
using FlightKit.DataAccess.Domain.Repo;

namespace FlightKit.DataAccess.Core.Services.Impl
{
    [NotTrackDbChange]
    public class FlightKitReportDataService : IFlightKitReportDataService
    {
        private readonly IDbRepository<Risk_Comment> _commentRepo;
        private readonly IDbRepository<Risk_Exposure> _exporeRepo;
        private readonly IDbRepository<Risk_FireDivisionRisk> _fireDivisionRepo;
        private readonly IDbRepository<Risk_FloorsAndRoof> _floorAndRoofRepo;
        private readonly IDbRepository<Risk_InternalProtection> _internalProtectionRepo;
        private readonly IDbRepository<Risk_Occupant> _occupantRepo;
        private readonly IDbRepository<Risk_ProtectionSafeguard> _safeGuardRepo;
        private readonly IDbRepository<Risk_ReportAddress> _addressRepo;
        private readonly IDbRepository<Risk_ReportAttachment> _attachmentRepo;
        private readonly IDbRepository<Risk_ReportBuildingInformation> _buildingInfoRepo;
        private readonly IDbRepository<Risk_ReportHazard> _reportHazardRepo;
        private readonly IDbRepository<Risk_ReportPhoto> _reportPhotoRepo;
        private readonly IDbRepository<Risk_ReportRelatedDate> _relatedDataRepo;
        private readonly IDbRepository<Risk_RetiredOccupantNumber> _retiredOccupantNumberRepo;
        private readonly IDbRepository<Risk_SecondaryConstruction> _secondaryConstructionRepo;
        private readonly IDbRepository<Risk_Wall> _wallRepo;
        private readonly IEnumerable<IDbRepository> _repos;
        private readonly IMappingHelperService _mapper;
        private readonly IDbRepository<Risk_Report> _riskReportRepo;
        private readonly IDbRepository<Risk_AdditionDate> _additionDateRepo;

        public FlightKitReportDataService(IDbRepository<Risk_Report> riskReportRepo,
            IDbRepository<Risk_AdditionDate> additionDateRepo,
            IDbRepository<Risk_Comment> commentRepo,
            IDbRepository<Risk_Exposure> exporeRepo,
            IDbRepository<Risk_FireDivisionRisk> fireDivisionRepo,
            IDbRepository<Risk_FloorsAndRoof> floorAndRoofRepo,
            IDbRepository<Risk_InternalProtection> internalProtectionRepo,
            IDbRepository<Risk_Occupant> occupantRepo,
            IDbRepository<Risk_ProtectionSafeguard> safeGuardRepo,
            IDbRepository<Risk_ReportAddress> addressRepo,
            IDbRepository<Risk_ReportAttachment> attachmentRepo,
            IDbRepository<Risk_ReportBuildingInformation> buildingInfoRepo,
            IDbRepository<Risk_ReportHazard> reportHazardRepo,
            IDbRepository<Risk_ReportPhoto> reportPhotoRepo,
            IDbRepository<Risk_ReportRelatedDate> relatedDataRepo,
            IDbRepository<Risk_RetiredOccupantNumber> retiredOccupantNumberRepo,
            IDbRepository<Risk_SecondaryConstruction> secondaryConstructionRepo,
            IDbRepository<Risk_Wall> wallRepo, IEnumerable<IDbRepository> repos,
            IMappingHelperService mapper)
        {
            _riskReportRepo = riskReportRepo;
            _additionDateRepo = additionDateRepo;
            _commentRepo = commentRepo;
            _exporeRepo = exporeRepo;
            _fireDivisionRepo = fireDivisionRepo;
            _floorAndRoofRepo = floorAndRoofRepo;
            _internalProtectionRepo = internalProtectionRepo;
            _occupantRepo = occupantRepo;
            _safeGuardRepo = safeGuardRepo;
            _addressRepo = addressRepo;
            _attachmentRepo = attachmentRepo;
            _buildingInfoRepo = buildingInfoRepo;
            _reportHazardRepo = reportHazardRepo;
            _reportPhotoRepo = reportPhotoRepo;
            _relatedDataRepo = relatedDataRepo;
            _retiredOccupantNumberRepo = retiredOccupantNumberRepo;
            _secondaryConstructionRepo = secondaryConstructionRepo;
            _wallRepo = wallRepo;
            _repos = repos;
            _mapper = mapper;
        }

        public async Task<RiskReport> GetRiskReportByReportIdAsync(Guid reportId, bool includesSyncMetadata = false)
        {
            var reports = await GetRiskReportsBy(r => r.ReportIdentifier == reportId, includesSyncMetadata).ConfigureAwait(false);
            return reports.FirstOrDefault();
        }

        public Task<ICollection<RiskReport>> GetRiskReportsByOrderIdAsync(long orderId, bool includesSyncMetadata = false)
        {
            return GetRiskReportsBy(r => r.OrderId == orderId, includesSyncMetadata);
        }

        public Task<ICollection<RiskReport>> GetRiskReportsByRiskIdAsync(string riskId, bool includesSyncMetadata = false)
        {
            return GetRiskReportsBy(r => r.RiskId == riskId);
        }


        public async Task<(ICollection<TDtoWithSyncMetadata> data, int totalReportsCount, Guid? reportCursor, bool hasNext)> GetRiskDataWithSyncMetadataAsync<TEntity, TDtoWithSyncMetadata>
            (Expression<Func<Risk_Report, bool>> filter,
            Expression<Func<Risk_Report, IEnumerable<TEntity>>> getDataExp, DateTime? lastSyncDateTime = null,
            Expression<Func<Risk_Report, IComparable>> orderby = null,
            bool? isascending = true, Guid? startId = null,
            int? first = null)
            where TEntity : class, IEntityWithSyncMetadata<Risk_SyncMetadata>, new()
            where TDtoWithSyncMetadata : RiskDtoWithSyncMetadata
        {
            var tableName = typeof(TEntity).GetCustomAttribute<TableNameAttribute>()?.TableName
                ?? typeof(TDtoWithSyncMetadata).Name;

            var (query, count, cursor, hasNext) = await ComposeRiskReportSelectQuery(filter, orderby, isascending, startId, first)
                                                    .ConfigureAwait(false);

            var finalQuery = query.SelectMany(getDataExp)
                .Where(d => d.RiskSyncMetadata.SyncTable == null || d.RiskSyncMetadata.SyncTable == tableName);

            if (lastSyncDateTime != null)
            {
                finalQuery = finalQuery.Where(d => d.RiskSyncMetadata.LastUpdateUtcDateTime > lastSyncDateTime.Value);
            }

            var data = await GetRepo<TEntity>().QueryAsync(_mapper.MapQueryableFromEntity<TEntity, TDtoWithSyncMetadata>(finalQuery, true)).ConfigureAwait(false);

            return (data, count, cursor, hasNext);
        }

        public async Task<(ICollection<TDtoWithSyncMetadata> data, int totalReportsCount, Guid? reportCursor, bool hasNext)> GetRiskDataWithSyncMetadataAsync<TEntity, TDtoWithSyncMetadata>
            (Expression<Func<Risk_Report, bool>> filter,
            Expression<Func<Risk_Report, TEntity>> getDataExp, DateTime? lastSyncDateTime = null,
            Expression<Func<Risk_Report, IComparable>> orderby = null,
            bool? isascending = true, Guid? startId = null,
            int? first = null)
            where TEntity : class, IEntityWithSyncMetadata<Risk_SyncMetadata>, new()
            where TDtoWithSyncMetadata : RiskDtoWithSyncMetadata
        {
            var tableName = typeof(TEntity).GetCustomAttribute<TableNameAttribute>()?.TableName
                ?? typeof(TDtoWithSyncMetadata).Name;

            var (query, count, cursor, hasNext) = await ComposeRiskReportSelectQuery(filter, orderby, isascending, startId, first)
                                                    .ConfigureAwait(false);

            var finalQuery = query.Select(getDataExp)
                .Where(d => d.RiskSyncMetadata.SyncTable == null || d.RiskSyncMetadata.SyncTable == tableName);

            if (lastSyncDateTime != null)
            {
                finalQuery = finalQuery.Where(d => d.RiskSyncMetadata.LastUpdateUtcDateTime > lastSyncDateTime.Value);
            }

            var data = await GetRepo<TEntity>().QueryAsync(_mapper.MapQueryableFromEntity<TEntity, TDtoWithSyncMetadata>(finalQuery, true)).ConfigureAwait(false);

            return (data, count, cursor, hasNext);
        }

        #region private methods
        private async Task<ICollection<RiskReport>> GetRiskReportsBy(Expression<Func<Risk_Report, bool>> filter,
            bool includesSyncMetadata = false)
        {
            var riskReports = await _riskReportRepo
                .QueryAsync(_mapper.MapQueryableFromEntity<Risk_Report, RiskReport>(_riskReportRepo.QueryBy(filter), includesSyncMetadata))
                .ConfigureAwait(false);

            var reportIds = riskReports.Select(r => r.ReportIdentifier).ToArray();
            var additionDateTask = GetChildrenByReportId<Risk_AdditionDate, RiskAdditionDate>(_additionDateRepo, includesSyncMetadata, reportIds);
            var commentsTask = GetChildrenByReportId<Risk_Comment, RiskComment>(_commentRepo, includesSyncMetadata, reportIds);
            var exposuresTask = GetChildrenByReportId<Risk_Exposure, RiskExposure>(_exporeRepo, includesSyncMetadata, reportIds);
            var fireDivisionTask = GetChildrenByReportId<Risk_FireDivisionRisk, RiskFireDivisionRisk>(_fireDivisionRepo, includesSyncMetadata, reportIds);
            var floorsAndRoofsTask = GetChildrenByReportId<Risk_FloorsAndRoof, RiskFloorsAndRoof>(_floorAndRoofRepo, includesSyncMetadata, reportIds);
            var internalProtectionTask = GetChildrenByReportId<Risk_InternalProtection, RiskInternalProtection>(_internalProtectionRepo, includesSyncMetadata, reportIds);
            var occupantsTask = GetChildrenByReportId<Risk_Occupant, RiskOccupant>(_occupantRepo, includesSyncMetadata, reportIds);
            var protectionSafeGuardTask = GetChildrenByReportId<Risk_ProtectionSafeguard, RiskProtectionSafeguard>(_safeGuardRepo, includesSyncMetadata, reportIds);
            var addressesTask = GetChildrenByReportId<Risk_ReportAddress, RiskReportAddress>(_addressRepo, includesSyncMetadata, reportIds);
            var attachmentsTask = GetChildrenByReportId<Risk_ReportAttachment, RiskReportAttachment>(_attachmentRepo, includesSyncMetadata, reportIds);
            var hazardsTask = GetChildrenByReportId<Risk_ReportHazard, RiskReportHazard>(_reportHazardRepo, includesSyncMetadata, reportIds);
            var buildingInfoTask = GetChildrenByReportId<Risk_ReportBuildingInformation, RiskReportBuildingInformation>(_buildingInfoRepo, includesSyncMetadata, reportIds);
            var photosTask = GetChildrenByReportId<Risk_ReportPhoto, RiskReportPhoto>(_reportPhotoRepo, includesSyncMetadata, reportIds);
            var relatedDatesTask = GetChildrenByReportId<Risk_ReportRelatedDate, RiskReportRelatedDate>(_relatedDataRepo, includesSyncMetadata, reportIds);
            var retiredOccupantNumbersTask = GetChildrenByReportId<Risk_RetiredOccupantNumber, RiskRetiredOccupantNumber>(_retiredOccupantNumberRepo, includesSyncMetadata, reportIds);
            var secondaryConstructionTask = GetChildrenByReportId<Risk_SecondaryConstruction, RiskSecondaryConstruction>(_secondaryConstructionRepo, includesSyncMetadata, reportIds);
            var wallsTask = GetChildrenByReportId<Risk_Wall, RiskWall>(_wallRepo, includesSyncMetadata, reportIds);

            await Task.WhenAll(additionDateTask, commentsTask, exposuresTask, fireDivisionTask,
                floorsAndRoofsTask, internalProtectionTask, occupantsTask, protectionSafeGuardTask,
                additionDateTask, attachmentsTask, hazardsTask, buildingInfoTask, photosTask,
                relatedDatesTask, retiredOccupantNumbersTask, secondaryConstructionTask, wallsTask).ConfigureAwait(false);

            // assign property
            var riskReportsWithChildren = riskReports.Select(r =>
            {
                r.AdditionDates = additionDateTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.Comments = commentsTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.Exposures = exposuresTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.FireDivisionRisks = fireDivisionTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.FloorsAndRoofs = floorsAndRoofsTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.InternalProtection = internalProtectionTask.Result.FirstOrDefault(_ => _.ReportIdentifier == r.ReportIdentifier);
                r.Occupants = occupantsTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.ProtectionSafeguards = protectionSafeGuardTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.ReportAddresses = addressesTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.ReportAttachments = attachmentsTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.ReportBuildingInformations = buildingInfoTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.ReportHazards = hazardsTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.ReportPhotoes = photosTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.ReportRelatedDates = relatedDatesTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.RetiredOccupantNumbers = retiredOccupantNumbersTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                r.SecondaryConstruction = secondaryConstructionTask.Result.FirstOrDefault(_ => _.ReportIdentifier == r.ReportIdentifier);
                r.Walls = wallsTask.Result.Where(_ => _.ReportIdentifier == r.ReportIdentifier).ToList();
                return r;
            }).ToList();

            return riskReportsWithChildren;
        }

        private async Task<ICollection<TDto>> GetChildrenByReportId<TEntity, TDto>(IDbRepository<TEntity> repo, bool includesSyncMetadata = false, params Guid[] riskreportIds)
            where TEntity : class, IFlightKitEntityWithReportId, new()
        {
            var query = repo.QueryBy(d => riskreportIds.Contains(d.ReportIdentifier));
            var dtoQuery = _mapper.MapQueryableFromEntity<TEntity, TDto>(query, includesSyncMetadata);

            var data = await GetRepo<TEntity>().QueryAsync(dtoQuery).ConfigureAwait(false);

            return data;
        }

        private async Task<(IQueryable<Risk_Report> query, int totalCount, Guid? cursor, bool hasNext)> ComposeRiskReportSelectQuery(Expression<Func<Risk_Report, bool>> filter,
            Expression<Func<Risk_Report, IComparable>> orderby = null,
            bool? isascending = true, Guid? startId = null,
            int? first = null)
        {
            var query = _riskReportRepo.QueryBy(filter);
            query = orderby == null ? query : (isascending == true ? query.OrderBy(orderby) : query.OrderByDescending(orderby));
            var allReportIds = await _riskReportRepo.QueryAsync(query.Select(q => q.ReportIdentifier)).ConfigureAwait(false);

            var totalCount = allReportIds.Count;
            int skipCount = startId != null ? allReportIds.TakeWhile(id => id != startId.Value).Count() + 1 : 0;

            query = query.Skip(skipCount);
            query = first == null ? query : query.Take(first.Value);

            var hasNext = first == null ? false : totalCount > (first.Value + skipCount);
            int leftCount = first == null ? totalCount - skipCount : Math.Min(totalCount - skipCount, first.Value);
            var cursor = hasNext ?
                allReportIds.Take(first.Value + skipCount).LastOrDefault() :
                allReportIds.LastOrDefault();

            return (query, leftCount, cursor, hasNext);
        }

        private IDbRepository<TEntity> GetRepo<TEntity>()
            where TEntity : class, new()
        {
            return _repos?.SingleOrDefault(t => t.GetType().GetGenericArguments()[0] == typeof(TEntity)) as IDbRepository<TEntity>;
        }
        #endregion
    }
}
