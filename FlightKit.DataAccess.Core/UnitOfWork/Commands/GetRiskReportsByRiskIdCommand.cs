using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.UnitOfWork.Commands
{
    public class GetRiskReportsByRiskIdCommand : ICommand
    {
        public string RiskId { get; set; }
        public bool IncludeSyncMetadata { get; set; }
    }
}
