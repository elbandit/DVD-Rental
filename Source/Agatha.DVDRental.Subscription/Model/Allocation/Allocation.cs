using System;
using System.Collections.Generic;
using Agatha.DVDRental.Domain;
using System.Linq;

namespace Agatha.DVDRental.Subscription.Model.Allocation
{
    public class Allocation
    {
        public Allocation(int filmId, int stock)
        {
            FilmId = filmId;
            Stock = stock;
        }

        public int Id { get; set; }
        public int FilmId { get; set; }
        public int Stock { get; set; }
        public int Available { get; set; }

        public IList<SubscriptionAllocation> SubscriptionAllocations { get; set; }

        // Don't need this as its only for decision..
        public bool StockAvailble()
        {
            throw new NotImplementedException();
        }

        public void IncreaseStock()
        {
            Stock++;
        }

        public void AllocateUnitTo(int subscriptionId)
        {                       
            if (!HasAllocatedFor(subscriptionId))
            {
                SubscriptionAllocations.Add(new SubscriptionAllocation(){ SubscriptionId = subscriptionId});

                DomainEvents.Raise(new FilmAllocated(FilmId, subscriptionId));
            }                      
        }

        public bool HasAllocatedFor(int subscriptionId)
        {
            return SubscriptionAllocations.Count(x => x.SubscriptionId == subscriptionId) > 0;
        }
    }
}
