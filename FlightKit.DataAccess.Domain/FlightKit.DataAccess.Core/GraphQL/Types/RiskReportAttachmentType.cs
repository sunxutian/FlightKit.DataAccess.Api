using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data.Entity;
using GraphQL.Types;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public class RiskReportAttachmentType : GenericGraphQLType<RiskReportAttachment>
    {
        public RiskReportAttachmentType() : base()
        {
        }
    }
}
