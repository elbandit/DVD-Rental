using System;
using Agatha.DVDRental.Domain;
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
        private readonly IBus _bus;

        public AllocateRentalRequestHandler(ISubscriptionRepository subscriptionRepository, 
                                            IRentalRepository rentalRepository,
                                            IAllocationRepository allocationRepository,
                                            IBus bus)
        {
            _subscriptionRepository = subscriptionRepository;
            _rentalRepository = rentalRepository;
            _allocationRepository = allocationRepository;
            _bus = bus;
        }

        public void Handle(AllocateRentalRequest message)
        {
            var subscription = _subscriptionRepository.FindBy(message.SubscriptionId);

            if (subscription != null)
            {
                var currentPeriodRentals = _rentalRepository.FindRentalsForCurrentPeriod();

                var currentAllocations = _allocationRepository.FindAllocationsFor(message.SubscriptionId);

                Allocation allocation = _allocationRepository.FindBy(message.FilmId);

                using (DomainEvents.Register(FilmAllocatedCallBack()))
                {
                    new AllocationService().Allocate(subscription, currentPeriodRentals, currentAllocations, allocation);
                }
            }
        }

        private Action<FilmAllocated> FilmAllocatedCallBack()
        {
            return (FilmAllocated s) => _bus.Publish(new FilmHasBeenAllocated() { FilmId = s.FilmId, SubscriptionId = s.SubscriptionId});
        }
    }
}
