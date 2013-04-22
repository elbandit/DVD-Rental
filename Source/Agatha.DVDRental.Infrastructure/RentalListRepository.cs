using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Domain.RentalLists;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Agatha.DVDRental.Infrastructure
{
    public class RentalListRepository : IRentalListRepository
    {
        private readonly IDocumentSession _documentSession;

        public RentalListRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public RentalList FindBy(int memberId)
        {
            IList<RentalRequest> results = new List<RentalRequest>();
          
            results = _documentSession.Query<RentalRequest>()
                    .Where(x => x.MemberId == memberId).ToList();
           
            return new RentalList(results);
        }

        public void Add(RentalRequest request)
        {           
             _documentSession.Store(request);                                                              
        }
    }
}
