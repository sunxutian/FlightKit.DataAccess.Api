using AutoMapper;
using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Application.Mapping
{
    public class FlightKitDomainToFlightKitApplicationWithNoSyncMetadataMappingProfile : Profile
    {
        public FlightKitDomainToFlightKitApplicationWithNoSyncMetadataMappingProfile() : base()
        {
            CreateMap<Risk_AdditionDate, RiskAdditionDate>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_Comment, RiskComment>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_CommentSegment, RiskCommentSegment>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_ConstructionTypeCode, RiskConstructionTypeCode>(MemberList.Destination)
                .ReverseMap();
            CreateMap<Risk_Exposure, RiskExposure>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_FireDivisionRisk, RiskFireDivisionRisk>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_FloorsAndRoof, RiskFloorsAndRoof>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_InternalProtection, RiskInternalProtection>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_Occupant, RiskOccupant>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_OccupantHazard, RiskOccupantHazard>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_OccupantLevel, RiskOccupantLevel>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_ProtectionSafeguard, RiskProtectionSafeguard>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_ProtectionSafeguardCode, RiskProtectionSafeguardCode>(MemberList.Destination)
                .ReverseMap();
            CreateMap<Risk_ReportAddress, RiskReportAddress>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_ReportAttachment, RiskReportAttachment>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_ReportBuildingInformation, RiskReportBuildingInformation>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_ReportDateTypeCode, RiskReportDateTypeCode>(MemberList.Destination).ReverseMap();
            CreateMap<Risk_ReportHazard, RiskReportHazard>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_ReportPhoto, RiskReportPhoto>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_ReportRelatedDate, RiskReportRelatedDate>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_RetiredOccupantNumber, RiskRetiredOccupantNumber>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_SecondaryConstruction, RiskSecondaryConstruction>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_Wall, RiskWall>(MemberList.Destination)
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap();
            CreateMap<Risk_SyncMetadata, RiskSyncMetadata>(MemberList.Destination).ReverseMap();
            CreateMap<Risk_SyncMetadataClient, RiskSyncMetadataClient>(MemberList.Destination).ReverseMap();

            CreateMap<Risk_Report, RiskReport>(MemberList.Destination)
                .ForMember(r => r.AdditionDates, config => config.Ignore())
                .ForMember(r => r.Comments, config => config.Ignore())
                .ForMember(r => r.Exposures, config => config.Ignore())
                .ForMember(r => r.FireDivisionRisks, config => config.Ignore())
                .ForMember(r => r.FloorsAndRoofs, config => config.Ignore())
                .ForMember(r => r.InternalProtection, config => config.Ignore())
                .ForMember(r => r.Occupants, config => config.Ignore())
                .ForMember(r => r.ProtectionSafeguards, config => config.Ignore())
                .ForMember(r => r.ReportAddresses, config => config.Ignore())
                .ForMember(r => r.ReportAttachments, config => config.Ignore())
                .ForMember(r => r.ReportBuildingInformations, config => config.Ignore())
                .ForMember(r => r.ReportHazards, config => config.Ignore())
                .ForMember(r => r.ReportPhotoes, config => config.Ignore())
                .ForMember(r => r.ReportRelatedDates, config => config.Ignore())
                .ForMember(r => r.RetiredOccupantNumbers, config => config.Ignore())
                .ForMember(r => r.SecondaryConstruction, config => config.Ignore())
                .ForMember(r => r.Walls, config => config.Ignore())
                .ForMember(c => c.RiskSyncMetadata, config => config.Ignore())
                .ForMember(c => c.ReportId, config => config.Ignore())
                .ReverseMap()
                .ForMember(r => r.InternalProtections,
                    config => config.ResolveUsing((dto, entity, member, context) =>
                    {
                        Risk_InternalProtection internalProtection = context.Mapper
                            .Map<Risk_InternalProtection>(dto.InternalProtection);

                        return new List<Risk_InternalProtection> { internalProtection };
                    }))
                .ForMember(r => r.SecondaryConstructions,
                   config => config.ResolveUsing((dto, entity, member, context) =>
                   {
                       Risk_SecondaryConstruction secondaryConstruction = context.Mapper
                        .Map<Risk_SecondaryConstruction>(dto.SecondaryConstruction);

                       return new List<Risk_SecondaryConstruction> { secondaryConstruction };
                   }));

        }
    }
}
