using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using Raven.Client;

namespace Agatha.DVDRental.Subscription.Infrastructure
{
    public class RentalRequestRepository : IRentalRequestRepository
    {
        private readonly IDocumentSession _documentSession;

        public RentalRequestRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public RentalRequestList FindBy(int subscriptionId)
        {
            return _documentSession.Load<RentalRequestList>(subscriptionId);
        }

        public void Add(RentalRequestList request)
        {           
             _documentSession.Store(request);                                                              
        }
    }
}
