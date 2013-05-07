using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Raven.Client;

namespace Agatha.DVDRental.Fulfillment.Infrastructure
{
    public class FulfilmentRepository : IFulfilmentRepository
    {
        private readonly IDocumentSession _documentSession;

        public FulfilmentRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<FulfilmentRequest> FindBy(int filmId, int subscriptionId)
        {
            return _documentSession.Query<FulfilmentRequest>().Where(
                x => x.FilmId == filmId && x.SubscriptionId == subscriptionId).ToList();
        }

        public void Add(FulfilmentRequest fulfilmentRequest)
        {
            _documentSession.Store(fulfilmentRequest);
        }

        public IEnumerable<FulfilmentRequest> FindAllAssignedTo(string pickerName)
        {
            return _documentSession.Query<FulfilmentRequest>().Where(
                x => x.AssignedTo == pickerName).ToList();
        }
        
        public IEnumerable<FulfilmentRequest> FindOldsetUnassignedTop(int number)
        {
            return _documentSession.Query<FulfilmentRequest>().OrderByDescending(x => x.Requested).Take(number).ToList();
        }
    }
}
