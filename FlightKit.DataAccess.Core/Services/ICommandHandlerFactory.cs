using FlightKit.DataAccess.Core.UnitOfWork.CommandHandlers;
using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Core.Services
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand, TResult> RequestCommandHandler<TCommand, TResult>()
            where TCommand : ICommand;
    }
}
