using FlightKit.DataAccess.Core.GraphQL;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightKit.DataAccess.Api.GraphQL
{
    public class FlightKitDataAccessSchema : Schema
    {
        public FlightKitDataAccessSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<FlightKitDataAccessQuery>();
            //Subscription = resolver.Resolve<FlightKistDataAccessSubscription>();
        }
    }
}
