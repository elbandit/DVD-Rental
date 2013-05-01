using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Subscription.Model.Allocation;
using Raven.Client;

namespace Agatha.DVDRental.Subscription.Infrastructure
{
    public class AllocationRepository : IAllocationRepository
    {
        private readonly IDocumentSession _documentSession;

        public AllocationRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Allocation FindBy(int filmId)
        {
            return _documentSession.Load<Allocation>(filmId);
        }

        public IEnumerable<Allocation> FindAllocationsFor(int subscriptionId)
        {
            return _documentSession.Query<Allocation>().Where(x => x.HasAllocatedFor(subscriptionId));
        }

        public void Add(Allocation filmAllocations)
        {
            _documentSession.Store(filmAllocations);
        }
    }
}
