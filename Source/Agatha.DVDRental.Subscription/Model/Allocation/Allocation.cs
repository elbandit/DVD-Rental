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
            Id = filmId;
            Stock = stock;
            SubscriptionAllocations = new List<SubscriptionAllocation>();
        }

        public int Id { get; set; }
        //public int FilmId { get; set; }
        
        public int Stock { get; set; }
        public int Available { get; set; }

        public IList<SubscriptionAllocation> SubscriptionAllocations { get; set; }

        public void IncreaseStock()
        {
            Stock++;
        }

        public void AllocateUnitTo(int subscriptionId)
        {                       
            if (!HasAllocatedFor(subscriptionId))
            {
                SubscriptionAllocations.Add(new SubscriptionAllocation(){ SubscriptionId = subscriptionId});

                DomainEvents.Raise(new FilmAllocated() { FilmId = Id, SubscriptionId = subscriptionId });
            }                      
        }

        public bool HasAllocatedFor(int subscriptionId)
        {
            return SubscriptionAllocations.Count(x => x.SubscriptionId == subscriptionId) > 0;
        }
    }
}
