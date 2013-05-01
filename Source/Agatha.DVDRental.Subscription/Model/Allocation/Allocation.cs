using System;
using Agatha.DVDRental.Domain;

namespace Agatha.DVDRental.Subscription.Model.Allocation
{
    public class Allocation
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int Stock { get; set; }
        public int Available { get; set; }

        // Don't need this as its only for decision..
        public bool StockAvailble()
        {
            throw new NotImplementedException();
        }

        public void AllocateUnitTo(int subscriptionId)
        {
            throw new NotImplementedException();

            // don't allocate the same film twice

            // New SubscriptionAllocation
            // Add to inner list

            DomainEvents.Raise(new FilmAllocated(FilmId, subscriptionId));
        }
    }
}
