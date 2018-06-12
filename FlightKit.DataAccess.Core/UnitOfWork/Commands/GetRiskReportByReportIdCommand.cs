using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.UnitOfWork.Commands
{
    public class GetRiskReportByReportIdCommand : ICommand
    {
        public Guid ReportId { get; set; }
        public bool IncludeSyncMetadata { get; set; }
    }
}
