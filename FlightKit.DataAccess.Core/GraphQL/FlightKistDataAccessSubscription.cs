using FlightKit.DataAccess.Application.Models;
using FlightKit.DataAccess.Core.GraphQL.Types;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using GraphQL.Types;
using System;
using System.Reactive.Linq;

namespace FlightKit.DataAccess.Core.GraphQL
{
    public class FlightKistDataAccessSubscription : ObjectGraphType<object>
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public FlightKistDataAccessSubscription(ICommandHandlerFactory commandHandlerFactory)
        {
            Name = "Subscription";
            _commandHandlerFactory = commandHandlerFactory;
        }
    }
}
