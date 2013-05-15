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

        public FulfilmentRequest FindBy(int filmId, int subscriptionId)
        {
            var id = String.Format("{0}-{1}", filmId, subscriptionId);

            return FindBy(id);
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
            return _documentSession.Query<FulfilmentRequest>().Where(x => x.AssignedTo == null).OrderByDescending(x => x.Requested).Take(number).ToList();
        }

        public FulfilmentRequest FindBy(string fulfilmentRequestId)
        {
            return _documentSession.Load<FulfilmentRequest>(fulfilmentRequestId);
        }
    }
}
