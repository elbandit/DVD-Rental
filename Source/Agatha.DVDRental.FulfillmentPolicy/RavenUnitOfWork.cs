using System;
using NServiceBus.UnitOfWork;
using Raven.Client;

namespace Agatha.DVDRental.FulfillmentPolicy
{
    public class RavenUnitOfWork : IManageUnitsOfWork
    {
        readonly IDocumentSession session;

        public RavenUnitOfWork(IDocumentSession session)
        {
            this.session = session;
        }

        public void Begin()
        {
        }


        public void End(Exception ex)
        {
            if (ex == null)
                session.SaveChanges();
        }
    }
}
