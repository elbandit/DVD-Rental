using Agatha.DVDRental.Subscription.Contracts;
using Agatha.DVDRental.Subscription.Model.Allocation;
using Agatha.DVDRental.Subscription.Model.RentalHistory;
using Agatha.DVDRental.Subscription.Model.Subscriptions;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy
{
    public class AllocateRentalRequestHandler : IHandleMessages<AllocateRentalRequest>
    {       
        private ISubscriptionRepository _subscriptionRepository;
        private IRentalRepository _rentalRepository;        
        private IAllocationRepository _allocationRepository;

        public AllocateRentalRequestHandler(ISubscriptionRepository subscriptionRepository, 
                                            IRentalRepository rentalRepository,
                                            IAllocationRepository allocationRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _rentalRepository = rentalRepository;
            _allocationRepository = allocationRepository;
        }

        public void Handle(AllocateRentalRequest message)
        {
            var subscription = _subscriptionRepository.FindBy(message.SubscriptionId);

            var currentPeriodRentals = _rentalRepository.FindRentalsForCurrentPeriod();

            var currentAllocations = _allocationRepository.FindAllocationsFor(message.SubscriptionId);

            Allocation allocation = _allocationRepository.FindBy(message.FilmId);
                        
            new AllocationService().Allocate(subscription, currentPeriodRentals, currentAllocations, allocation);
        }
    }
}
