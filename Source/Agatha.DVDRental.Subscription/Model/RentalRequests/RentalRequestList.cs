using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Domain;
using Agatha.DVDRental.Domain.RentalLists;

namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public class RentalRequestList
    {
        public int SubscriptionId { get; private set; }
        private IList<RentalRequest> _rentalRequests;

        public RentalRequestList(IList<RentalRequest> rentalRequests, int subscriptionId)
        {
            SubscriptionId = subscriptionId;
            _rentalRequests = rentalRequests;
        }

        public RentalRequest CreateRequestFor(int filmId, int memberid)
        {
            // make sure we don't already have this in the list

            // does this list have an age restriction?

            // give it a priority order
            var request = new RentalRequest(filmId, memberid);

            DomainEvents.Raise(new FilmRequested(filmId, memberid));

            return request;
        }

        public RentalRequest RemoveFromTheList(int filmId)
        {
            DomainEvents.Raise(new RentalRequestRemoved(filmId, SubscriptionId));

            return _rentalRequests.SingleOrDefault(x => x.FilmId == filmId);         
        }

        public RentalRequest ForRequestOf(int filmId)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RentalRequestRemoved
    {
        public int FilmId { get; set; }
        public int MemberId { get; set; }

        public RentalRequestRemoved(int filmId, int memberId)
        {
            FilmId = filmId;
            MemberId = memberId;
        }
    }
}
