using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Subscription.Contracts;
using Agatha.DVDRental.Subscription.Contracts.Events;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy
{
    public class SubscriptionCancellation : IHandleMessages<CannotCancelSubscribtion>
    {
        // I Start Saga (Process Manager)
        public void Handle(CannotCancelSubscribtion message)
        {
            // Start the saga (process manager)
        }
      
        public void Handle(SubscriptionCancelled message)
        {
            // kill the saga
        }

        public void Handle(FilmReturned message)
        {                     
            // Subscription.Cancel() // This will fire an event so that billing will cancel

            // Kill saga
        }

        public void Handle(CustomerWantsToKeepSubscription message)
        {
            // Kill saga
        }
    }
}
