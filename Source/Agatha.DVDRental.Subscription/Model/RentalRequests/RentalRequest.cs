using System;

namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public class RentalRequest
    {      
        public RentalRequest(int filmId, int subscriptionId)
        {
            Id = Guid.NewGuid().ToString();
            FilmId = filmId;
            SubscriptionId = subscriptionId;
            Requested = DateTime.Now;
            IsBeingPicked = false;
        }

        public string Id { get; private set; }

        public int FilmId { get; private set; }

        public int SubscriptionId { get; private set; }

        public DateTime Requested { get; private set; }

        public bool IsBeingPicked { get; private set; }        

        public void IsBeingPickedForDispatch()
        {
            IsBeingPicked = true;
        }

        public bool CanBeRemovedFromList
        {
            get { return !IsBeingPicked; }

        }
    }
}
