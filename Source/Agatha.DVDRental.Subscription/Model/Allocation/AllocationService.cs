using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Subscription.Model.RentalHistory;

namespace Agatha.DVDRental.Subscription.Model.Allocation
{
    public class AllocationService
    {

        public void Allocate(Subscriptions.Subscription subscription, 
                             CurrentPeriodRentals currentPeriodRentals, 
                             IEnumerable<Allocation> currentAllocations,
                             Allocation allocation)
        {
            if (subscription.IsEligibleToRecieveAFilm(currentPeriodRentals, currentAllocations))
            {               
                
                    allocation.AllocateUnitTo(subscription.Id); 
               
            }
        }
    }
}
