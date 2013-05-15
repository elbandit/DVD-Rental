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
            if (!IsContainedInTheList(filmId) )
            {
                // does this list have an age restriction?

                // give it a priority order
                var request = new RentalRequest(filmId, Id);

                RentalRequests.Add(request);

                DomainEvents.Raise(new FilmRequested(filmId, Id));
            }         
        }

        public void RemoveFromTheList(int filmId)
        {
            if (!IsContainedInTheList(filmId))
            {                
                RentalRequest request = RentalRequests.SingleOrDefault(x => x.FilmId == filmId);
          
                RentalRequests.Remove(request);

                DomainEvents.Raise(new RentalRequestRemoved(filmId, Id));
            }
        }

        private bool IsContainedInTheList(int filmId)
        {
            return RentalRequests.Count(x => x.FilmId == filmId) > 0;
        }

        public void MarkAsReadyForDispatch(int filmId)
        {
            if (IsContainedInTheList(filmId))
                RentalRequests.SingleOrDefault(x => x.FilmId == filmId).IsBeingPickedForDispatch();
        }

        public void Fulfilled(int filmId)
        {
            RentalRequest request = RentalRequests.SingleOrDefault(x => x.FilmId == filmId);

            RentalRequests.Remove(request);
        }
    }
}
