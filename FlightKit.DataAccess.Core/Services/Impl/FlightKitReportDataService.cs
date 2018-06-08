using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        private readonly IDbRepository<Risk_SyncMetadata> _syncMetadataRepo;
        private readonly IDbRepository<Risk_CommentSegment> _segmentRepo;
        private readonly IDbRepository<Risk_OccupantLevel> _occupantLevelRepo;
        private readonly IDbRepository<Risk_OccupantHazard> _occupantHazardRepo;
        private readonly IMappingHelperService _mapper;
        private readonly IDbRepository<Risk_Report> _riskReportRepo;
        private readonly IDbRepository<Risk_AdditionDate> _additionDataRepo;

        public FlightKitReportDataService(IDbRepository<Risk_Report> riskReportRepo,
            IDbRepository<Risk_AdditionDate> additionDataRepo,
            IDbRepository<Risk_Comment> commentRepo,
            IDbRepository<Risk_CommentSegment> segmentRepo,
            IDbRepository<Risk_Exposure> exporeRepo,
            IDbRepository<Risk_FireDivisionRisk> fireDivisionRepo,
            IDbRepository<Risk_FloorsAndRoof> floorAndRoofRepo,
            IDbRepository<Risk_InternalProtection> internalProtectionRepo,
            IDbRepository<Risk_Occupant> occupantRepo,
            IDbRepository<Risk_OccupantLevel> occupantLevelRepo,
            IDbRepository<Risk_OccupantHazard> occupantHazardRepo,
            IDbRepository<Risk_ProtectionSafeguard> safeGuardRepo,
            IDbRepository<Risk_ReportAddress> addressRepo,
            IDbRepository<Risk_ReportAttachment> attachmentRepo,
            IDbRepository<Risk_ReportBuildingInformation> buildingInfoRepo,
            IDbRepository<Risk_ReportHazard> reportHazardRepo,
            IDbRepository<Risk_ReportPhoto> reportPhotoRepo,
            IDbRepository<Risk_ReportRelatedDate> relatedDataRepo,
            IDbRepository<Risk_RetiredOccupantNumber> retiredOccupantNumberRepo,
            IDbRepository<Risk_SecondaryConstruction> secondaryConstructionRepo,
            IDbRepository<Risk_Wall> wallRepo,
            IDbRepository<Risk_SyncMetadata> syncMetadataRepo,
            IMappingHelperService mapper)
        {
            _riskReportRepo = riskReportRepo;
            _additionDataRepo = additionDataRepo;
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
            _syncMetadataRepo = syncMetadataRepo;
            _segmentRepo = segmentRepo;
            _occupantLevelRepo = occupantLevelRepo;
            _occupantHazardRepo = occupantHazardRepo;
            _mapper = mapper;
        }

        public async Task<RiskReport> GetRiskReportByReportId(Guid reportId)
        {
            var reports = await GetRiskReportsBy(r => r.ReportIdentifier == reportId).ConfigureAwait(false);
            return reports.FirstOrDefault();
        }

        public Task<ICollection<RiskReport>> GetRiskReportsByOrderId(long orderId)
        {
            return GetRiskReportsBy(r => r.OrderId == orderId);
        }

        public Task<ICollection<RiskReport>> GetRiskReportsByRiskId(string riskId)
        {
            return GetRiskReportsBy(r => r.RiskId == riskId);
        }

        private async Task<ICollection<RiskReport>> GetRiskReportsBy(Expression<Func<Risk_Report, bool>> filter)
        {
            var riskReports = await _mapper.GetMappedDtoFromDbWithSyncMetadataAsync<Risk_SyncMetadata, 
                RiskSyncMetadata, Risk_Report, RiskReport, Guid>(_riskReportRepo, _syncMetadataRepo,
                r => r.ReportIdentifier, filter)
                .ConfigureAwait(false);
            var reportIds = riskReports.Select(r => r.ReportIdentifier).ToArray();
            var additionDateTask = GetChildrenByReportIdAsync<Risk_AdditionDate, RiskAdditionDate, Guid>(_additionDataRepo, _ => _.AdditionDateIdentifier, reportIds);

            var commentsTask = GetChildrenByReportIdAsync<Risk_Comment, RiskComment, Guid>(_commentRepo, _ => _.CommentIdentifier, reportIds)
                .ContinueWith(async t =>
                {
                    var comments = t.Result;
                    var commentIds = comments.Select(c => c.CommentIdentifier);
                    var allSegments = await _mapper.GetMappedDtoFromDbWithSyncMetadataAsync<Risk_SyncMetadata,
                        RiskSyncMetadata, Risk_CommentSegment, RiskCommentSegment, Guid>(
                        _segmentRepo, _syncMetadataRepo, c => c.CommentSegmentIdentifier,
                        s => commentIds.Contains(s.CommentIdentifier)).ConfigureAwait(false);

                    foreach (var comment in comments)
                    {
                        comment.CommentSegments = allSegments.Where(s => s.CommentIdentifier == comment.CommentIdentifier).ToList();
                    }

                    return comments;

                }).Unwrap();

            var exposuresTask = GetChildrenByReportIdAsync<Risk_Exposure, RiskExposure, Guid>(_exporeRepo, _ => _.ExposureIdentifier, reportIds);
            var fireDivisionTask = GetChildrenByReportIdAsync<Risk_FireDivisionRisk, RiskFireDivisionRisk, Guid>(_fireDivisionRepo, _ => _.FireDivisionRiskIdentifier, reportIds);
            var floorsAndRoofsTask = GetChildrenByReportIdAsync<Risk_FloorsAndRoof, RiskFloorsAndRoof, Guid>(_floorAndRoofRepo, _ => _.FloorAndRoofIdentifier, reportIds);
            var internalProtectionTask = GetChildrenByReportIdAsync<Risk_InternalProtection, RiskInternalProtection, Guid>(_internalProtectionRepo, _ => _.InternalProtectionIdentifier, reportIds);
            var occupantsTask = GetChildrenByReportIdAsync<Risk_Occupant, RiskOccupant, Guid>(_occupantRepo, _ => _.OccupantIdentifier, reportIds)
                .ContinueWith(async t =>
                {
                    var occupants = t.Result;
                    var occupantIds = occupants.Select(o => o.OccupantIdentifier);

                    var allOccupantLevels = await _mapper.GetMappedDtoFromDbWithSyncMetadataAsync<Risk_SyncMetadata,
                        RiskSyncMetadata, Risk_OccupantLevel, RiskOccupantLevel, Guid>(
                        _occupantLevelRepo, _syncMetadataRepo, c => c.OccupantLevelIdentifier,
                        s => occupantIds.Contains(s.OccupantIdentifier)).ConfigureAwait(false);

                    var allOccupantHazards = await _mapper.GetMappedDtoFromDbWithSyncMetadataAsync<Risk_SyncMetadata,
                        RiskSyncMetadata, Risk_OccupantHazard, RiskOccupantHazard, Guid>(
                        _occupantHazardRepo, _syncMetadataRepo, c => c.OccupantHazardIdentifier,
                        s => occupantIds.Contains(s.OccupantIdentifier)).ConfigureAwait(false);

                    foreach (var occupant in occupants)
                    {
                        occupant.OccupantLevels = allOccupantLevels
                            .Where(l => l.OccupantIdentifier == occupant.OccupantIdentifier).ToList();

                        occupant.OccupantHazards = allOccupantHazards
                            .Where(l => l.OccupantIdentifier == occupant.OccupantIdentifier).ToList();
                    }

                    return occupants;

                }).Unwrap();

            var protectionSafeGuardTask = GetChildrenByReportIdAsync<Risk_ProtectionSafeguard, RiskProtectionSafeguard, Guid>(_safeGuardRepo, _ => _.ProtectionSafeguardIdentifier, reportIds);
            var addressesTask = GetChildrenByReportIdAsync<Risk_ReportAddress, RiskReportAddress, Guid>(_addressRepo, _ => _.ReportAddressIdentifier, reportIds);
            var attachmentsTask = GetChildrenByReportIdAsync<Risk_ReportAttachment, RiskReportAttachment, Guid>(_attachmentRepo, _ => _.ReportAttachmentIdentifier, reportIds);
            var hazardsTask = GetChildrenByReportIdAsync<Risk_ReportHazard, RiskReportHazard, Guid>(_reportHazardRepo, _ => _.ReportHazardIdentifier, reportIds);
            var buildingInfoTask = GetChildrenByReportIdAsync<Risk_ReportBuildingInformation, RiskReportBuildingInformation, Guid>(_buildingInfoRepo, _ => _.ReportBuildingInformationIdentifier, reportIds);
            var photosTask = GetChildrenByReportIdAsync<Risk_ReportPhoto, RiskReportPhoto, Guid>(_reportPhotoRepo, _ => _.ReportPhotoIdentifier, reportIds);
            var relatedDatesTask = GetChildrenByReportIdAsync<Risk_ReportRelatedDate, RiskReportRelatedDate, Guid>(_relatedDataRepo, _ => _.ReportRelatedDateIdentifier, reportIds);
            var retiredOccupantNumbersTask = GetChildrenByReportIdAsync<Risk_RetiredOccupantNumber, RiskRetiredOccupantNumber, Guid>(_retiredOccupantNumberRepo, _ => _.RetiredOccupantNumberIdentifier, reportIds);
            var secondaryConstructionTask = GetChildrenByReportIdAsync<Risk_SecondaryConstruction, RiskSecondaryConstruction, Guid>(_secondaryConstructionRepo, _ => _.SecondaryConstructionIdentifier, reportIds);
            var wallsTask = GetChildrenByReportIdAsync<Risk_Wall, RiskWall, Guid>(_wallRepo, _ => _.WallIdentifier, reportIds);

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

        private async Task<ICollection<TDto>> GetChildrenByReportIdAsync<TEntity, TDto, TPk>(IDbRepository<TEntity> repo,
            Func<TDto, TPk> pkSelector,
            params Guid[] riskreportIds)
            where TEntity : class, IFlightKitEntityWithReportId, IEntityWithSyncMetadata<Risk_SyncMetadata>, new()
            where TDto : class, IDtoWithSyncMetadata<RiskSyncMetadata>, new()
        {
            var data = await _mapper.GetMappedDtoFromDbWithSyncMetadataAsync<Risk_SyncMetadata, RiskSyncMetadata, TEntity, TDto, TPk>(repo, _syncMetadataRepo, pkSelector,
                d => riskreportIds.Contains(d.ReportIdentifier)).ConfigureAwait(false);

            return data;
        }
    }
}
