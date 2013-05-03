using System;
using Agatha.DVDRental.Fulfillment.Infrastructure;

namespace Agatha.DVDRental.Fulfillment.Model.Fulfilment
{
    public class FulfilmentRequest
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime Requested { get; set; }
        public bool IsDispatched { get; private set; }

        public string AssignedTo { get; private set; } // the person who is going to pick the Dvd. Fires 'Ready to dispatch' event   
     
        public void AssignForPickingTo(string picker)
        {
            // can assign?
            AssignedTo = picker;

            DomainEvents.Raise(new FulfilmentRequestAssignedForPicking());
        }

        public void Dispatched()
        {
            IsDispatched = true;
            DomainEvents.Raise(new FulfilmentRequestDispatched());
        }
    }
}
