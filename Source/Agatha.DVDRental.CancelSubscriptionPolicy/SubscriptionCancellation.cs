using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace Agatha.DVDRental.CancelSubscriptionPolicy
{
    public class SubscriptionCancellation :  IHandleMessages<CannotCancelSubscription>
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

        public void Handle(DVDReturned message)
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
