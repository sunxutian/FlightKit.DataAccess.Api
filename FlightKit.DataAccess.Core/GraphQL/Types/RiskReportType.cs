using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Domain.Data.Entity;
using FlightKit.DataAccess.Domain.Repo;
using System;
using System.Linq;
using GraphQL.Types;
namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskReportType : GenericGraphQLType<RiskReport>
    {
        public RiskReportType(Func<IDbRepository<Risk_Report>> repo,
            Func<IDbRepository<Risk_AdditionDate>> additionDateRepo, Func<IDbRepository<Risk_Comment>> commentRepo, Func<IDbRepository<Risk_Exposure>> exposureRepo,
            Func<IDbRepository<Risk_FireDivisionRisk>> fireDivisionRepo, Func<IDbRepository<Risk_FloorsAndRoof>> floorAndRoofRepo, Func<IDbRepository<Risk_InternalProtection>> internalProtectionRepo,
            Func<IDbRepository<Risk_Occupant>> occupantRepo, Func<IDbRepository<Risk_ProtectionSafeguard>> safeguardRepo, Func<IDbRepository<Risk_ReportAddress>> addressRepo,
            Func<IDbRepository<Risk_ReportAttachment>> attachmentRepo, Func<IDbRepository<Risk_ReportBuildingInformation>> buildingInfoRepo, Func<IDbRepository<Risk_ReportHazard>> reportHazardRepo,
            Func<IDbRepository<Risk_ReportPhoto>> reportPhotoRepo, Func<IDbRepository<Risk_ReportRelatedDate>> relatedDateRepo, Func<IDbRepository<Risk_RetiredOccupantNumber>> retiredOccupantNumberRepo,
            Func<IDbRepository<Risk_SecondaryConstruction>> secondaryConstructionRepo, Func<IDbRepository<Risk_Wall>> wallRepo,
            IMappingHelperService mapper) : base()
        {
            FieldAsync<ListGraphType<RiskAdditionDateType>>("riskAdditionDates",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_AdditionDate, RiskAdditionDate>(additionDateRepo(), a => a.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskCommentType>>("riskComments", 
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_Comment, RiskComment>(commentRepo(), c => c.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskExposureType>>("riskExposures",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_Exposure, RiskExposure>(exposureRepo(), e => e.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskFireDivisionRiskType>>("riskFireDivisionRisks",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_FireDivisionRisk, RiskFireDivisionRisk>(fireDivisionRepo(), f => f.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskFloorsAndRoofType>>("riskFloorsAndRoofs",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_FloorsAndRoof, RiskFloorsAndRoof>(floorAndRoofRepo(), f => f.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<RiskInternalProtectionType>("riskInternalProtection",
                resolve: async context => (await mapper.GetMappedDtoFromDbAsync<Risk_InternalProtection, RiskInternalProtection>(internalProtectionRepo(), i => i.ReportIdentifier == context.Source.ReportIdentifier)).FirstOrDefault());
            FieldAsync<ListGraphType<RiskOccupantType>>("riskOccupants",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_Occupant, RiskOccupant>(occupantRepo(), o => o.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskProtectionSafeguardType>>("riskProtectionSafeguards",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_ProtectionSafeguard, RiskProtectionSafeguard>(safeguardRepo(), s => s.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskReportAddressType>>("riskReportAddresses",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_ReportAddress, RiskReportAddress>(addressRepo(), a => a.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskReportAttachmentType>>("riskReportAttachments",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_ReportAttachment, RiskReportAttachment>(attachmentRepo(), a => a.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskReportBuildingInformationType>>("riskBuildingInformations",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_ReportBuildingInformation, RiskReportBuildingInformation>(buildingInfoRepo(), b => b.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskReportHazardType>>("riskReportHazards",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_ReportHazard, RiskReportHazard>(reportHazardRepo(), r => r.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskReportPhotoType>>("riskReportPhotos",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_ReportPhoto, RiskReportPhoto>(reportPhotoRepo(), p => p.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskReportRelatedDateType>>("riskReportRelatedDates",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_ReportRelatedDate, RiskReportRelatedDate>(relatedDateRepo(), p => p.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<ListGraphType<RiskRetiredOccupantNumberType>>("riskRetiredOccupantNumbers",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_RetiredOccupantNumber, RiskRetiredOccupantNumber>(retiredOccupantNumberRepo(), r => r.ReportIdentifier == context.Source.ReportIdentifier));
            FieldAsync<RiskSecondaryConstructionType>("riskSecondaryConstruction",
              resolve: async context => (await mapper.GetMappedDtoFromDbAsync<Risk_SecondaryConstruction, RiskSecondaryConstruction>(secondaryConstructionRepo(), r => r.ReportIdentifier == context.Source.ReportIdentifier)).FirstOrDefault());
            FieldAsync<ListGraphType<RiskWallType>>("riskWalls",
                resolve: async context => await mapper.GetMappedDtoFromDbAsync<Risk_Wall, RiskWall>(wallRepo(), w => w.ReportIdentifier == context.Source.ReportIdentifier));
        }
    }
}
