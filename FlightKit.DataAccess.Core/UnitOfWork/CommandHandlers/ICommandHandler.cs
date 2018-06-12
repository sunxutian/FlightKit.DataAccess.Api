using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.UnitOfWork.CommandHandlers
{
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
