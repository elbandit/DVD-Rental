using System;
using Agatha.DVDRental.Infrastructure;

namespace Agatha.DVDRental.Fulfillment.Model.Fulfilment
{
    public class FulfilmentRequest
    {
        public FulfilmentRequest(int filmId, int subscriptionId)
        {
            this.FilmId = filmId;
            this.SubscriptionId = subscriptionId;
            Requested = DateTime.Now;
            Id = String.Format("{0}-{1}", filmId, subscriptionId); // Idempotent we can't create duplicate FulfilmentRequests
        }

        public string Id { get; set; }
        public int FilmId { get; private set; }
        public int SubscriptionId { get; private set; }
        public DateTime Requested { get; private set; }
        public bool IsDispatched { get;  set; }

        public string AssignedTo { get;  set; }

        public bool IsAssigned()
        {
            return !String.IsNullOrEmpty(AssignedTo);
        }

        public void AssignForPickingTo(string picker)
        {
            if (String.IsNullOrEmpty(AssignedTo))
            {
                AssignedTo = picker;

                DomainEvents.Raise(new FulfilmentRequestAssignedForPicking() { FilmId = FilmId, SubscriptionId = SubscriptionId });
            }           
        }

        public void FulfilledWith(int dvdId)
        {
            if (!IsDispatched)
            {
                IsDispatched = true;

                DomainEvents.Raise(new FulfilmentRequestDispatched() { FilmId = FilmId, SubscriptionId = SubscriptionId, DvdId = dvdId });
            }           
        }
    }
}
