using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Domain.RentalLists;
using Agatha.DVDRental.Domain.Subscriptions.RentalRequests;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Agatha.DVDRental.Infrastructure
{
    public class RentalRequestRepository : IRentalRequestRepository
    {
        private readonly IDocumentSession _documentSession;

        public RentalRequestRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public RentalRequestList FindBy(int memberId)
        {
            IList<RentalRequest> results = new List<RentalRequest>();
          
            results = _documentSession.Query<RentalRequest>()
                    .Where(x => x.MemberId == memberId).ToList();

            return new RentalRequestList(results, memberId);
        }

        public void Add(RentalRequest request)
        {           
             _documentSession.Store(request);                                                              
        }
    }
}
