using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Domain.Data;
using FlightKit.DataAccess.Domain.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.UnitOfWork.Commands
{
    public class GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto> : ICommand
        where TEntity : RiskEntityWithSyncMetadata, new()
        where TDto : RiskDtoWithSyncMetadata
    {
        public DateTime? LastSyncDateTime { get; }
        public Expression<Func<Risk_Report, bool>> Filter { get; }
        public Expression<Func<Risk_Report, TEntity>> GetDataExp { get; }
        public Expression<Func<Risk_Report, IEnumerable<TEntity>>> GetDataCollectionExp { get; }
        public Expression<Func<Risk_Report, IComparable>> OrderBy { get; }
        public bool? IsAscending { get; }
        public Guid? StartReportId { get; }
        public int? TakeNumber { get; }
        internal bool IsAccessingDataCollection { get; }

        public GetRiskSyncMetadataByRiskReportFilterCommand(Expression<Func<Risk_Report, bool>> filter, 
            Expression<Func<Risk_Report, TEntity>> getDataExp, DateTime? lastSyncDateTime = null, 
            Expression<Func<Risk_Report, IComparable>> orderby = null,
            bool? isascending = true, Guid? startId = null, int? first = null)
        {
            Filter = filter;
            LastSyncDateTime = LastSyncDateTime;
            GetDataExp = getDataExp;
            OrderBy = orderby;
            IsAscending = isascending;
            StartReportId = startId;
            TakeNumber = first;
            IsAccessingDataCollection = false;
        }

        public GetRiskSyncMetadataByRiskReportFilterCommand(Expression<Func<Risk_Report, bool>> filter, 
            Expression<Func<Risk_Report, IEnumerable<TEntity>>> getDataCollectionExp, DateTime? lastSyncDateTime = null,
            Expression<Func<Risk_Report, IComparable>> orderby = null,
            bool? isascending = true, Guid? startId = null, int? first = null)
        {
            Filter = filter;
            LastSyncDateTime = LastSyncDateTime;
            GetDataCollectionExp = getDataCollectionExp;
            OrderBy = orderby;
            IsAscending = isascending;
            StartReportId = startId;
            TakeNumber = first;
            IsAccessingDataCollection = true;
        }
    }
}
