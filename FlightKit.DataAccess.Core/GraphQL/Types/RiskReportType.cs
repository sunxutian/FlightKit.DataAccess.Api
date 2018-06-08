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
        public RiskReportType() : base()
        {
            Field<ListGraphType<RiskAdditionDateType>>("riskAdditionDates",
                resolve: context => context.Source.AdditionDates);
            Field<ListGraphType<RiskCommentType>>("riskComments", 
                resolve: context => context.Source.Comments);
            Field<ListGraphType<RiskExposureType>>("riskExposures",
                resolve: context => context.Source.Exposures);
            Field<ListGraphType<RiskFireDivisionRiskType>>("riskFireDivisionRisks",
                resolve: context => context.Source.FireDivisionRisks);
            Field<ListGraphType<RiskFloorsAndRoofType>>("riskFloorsAndRoofs",
                resolve: context => context.Source.FloorsAndRoofs);
            Field<RiskInternalProtectionType>("riskInternalProtection",
                resolve: context => context.Source.InternalProtection);
            Field<ListGraphType<RiskOccupantType>>("riskOccupants",
                resolve: context => context.Source.Occupants);
            Field<ListGraphType<RiskProtectionSafeguardType>>("riskProtectionSafeguards",
                resolve: context => context.Source.ProtectionSafeguards);
            Field<ListGraphType<RiskReportAddressType>>("riskReportAddresses",
                resolve: context => context.Source.ReportAddresses);
            Field<ListGraphType<RiskReportAttachmentType>>("riskReportAttachments",
                resolve: context => context.Source.ReportAttachments);
            Field<ListGraphType<RiskReportBuildingInformationType>>("riskBuildingInformations",
                resolve: context => context.Source.ReportBuildingInformations);
            Field<ListGraphType<RiskReportHazardType>>("riskReportHazards",
                resolve: context => context.Source.ReportHazards);
            Field<ListGraphType<RiskReportPhotoType>>("riskReportPhotos",
                resolve: context => context.Source.ReportPhotoes);
            Field<ListGraphType<RiskReportRelatedDateType>>("riskReportRelatedDates",
                resolve: context => context.Source.ReportRelatedDates);
            Field<ListGraphType<RiskRetiredOccupantNumberType>>("riskRetiredOccupantNumbers",
                resolve: context => context.Source.RetiredOccupantNumbers);
            Field<RiskSecondaryConstructionType>("riskSecondaryConstruction",
              resolve: context => context.Source.SecondaryConstruction);
            Field<ListGraphType<RiskWallType>>("riskWalls",
                resolve: context => context.Source.Walls);
        }
    }
}
