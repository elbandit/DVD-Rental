using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Domain;

namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public class RentalRequestList
    {
        public int Id { get; private set; }        
        public IList<RentalRequest> RentalRequests;

        public RentalRequestList(int subscriptionId)
        {
            Id = subscriptionId;
            RentalRequests = new List<RentalRequest>();
        }

        public void CreateRequestFor(int filmId)
        {
            // make sure we don't already have this in the list

            // does this list have an age restriction?

            // give it a priority order
            var request = new RentalRequest(filmId, Id);

            RentalRequests.Add(request);

            DomainEvents.Raise(new FilmRequested(filmId, Id));
        }

        public void RemoveFromTheList(int filmId)
        {
            DomainEvents.Raise(new RentalRequestRemoved(filmId, Id));

            RentalRequest request = RentalRequests.SingleOrDefault(x => x.FilmId == filmId);
      
            if(request != null)
            {
                RentalRequests.Remove(request);
            }
        }

        public void MarkAsReadyForDispatch(int filmId)
        {
            RentalRequests.SingleOrDefault(x => x.FilmId == filmId).IsBeingPickedForDispatch();
        }
    }
}
