using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;
using GraphQL.Types;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskReportRelatedDateType : GenericGraphQLType<RiskReportRelatedDate>
    {
        public RiskReportRelatedDateType() : base()
        {
            Field(r => r.ReportDateTypeCode, type: typeof(RiskReportDateTypeCodeType));
        }
    }
}
