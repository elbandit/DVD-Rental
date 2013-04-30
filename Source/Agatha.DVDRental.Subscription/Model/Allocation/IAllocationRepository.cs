using System.Collections.Generic;

namespace Agatha.DVDRental.Subscription.Model.Allocation
{
    public interface IAllocationRepository
    {
        Allocation FindBy(int filmId);
        IEnumerable<Allocation> FindAllocationsFor(int subscriptionId);
    }
}
