using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus.UnitOfWork;
using Raven.Client;

namespace Agatha.DVDRental.AllocationPolicy
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
