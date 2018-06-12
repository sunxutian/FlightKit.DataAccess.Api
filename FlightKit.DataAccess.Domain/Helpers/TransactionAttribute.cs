using System;
using System.Data;

namespace FlightKit.DataAccess.Domain.Helpers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class TransactionAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the isolation level of the db transaction .
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public IsolationLevel TransactionType { get; set; }
    }
}
