using Agatha.DVDRental.Domain.Allocation;
using Agatha.DVDRental.Domain.Fulfilment;
using Agatha.DVDRental.Domain.RentalHistory;
using Agatha.DVDRental.Domain.Subscriptions;
using Agatha.DVDRental.Messages.OperationalScenarios.Commands;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy
{
    public class AllocateDvdToRentalRequestHandler : IHandleMessages<AllocateRentalRequest>
    {       
        private ISubscriptionRepository _subscriptionRepository;
        private IRentalRepository _rentalRepository;
        private IFulfilmentRepository _fulfilmentRepository;
        private IAllocationRepository _allocationRepository;

        public AllocateDvdToRentalRequestHandler(ISubscriptionRepository subscriptionRepository, 
                                                 IRentalRepository rentalRepository, 
                                                 IFulfilmentRepository fulfilmentRepository, 
                                                 IAllocationRepository allocationRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _rentalRepository = rentalRepository;
            _fulfilmentRepository = fulfilmentRepository;
            _allocationRepository = allocationRepository;
        }

        public void Handle(AllocateRentalRequest message)
        {
            Subscription subscription = _subscriptionRepository.FindBy(message.SubscriptionId);

            var currentPeriodRentals = _rentalRepository.FindRentalsForCurrentPeriod();

            var currentAllocations = _fulfilmentRepository.FindBy(message.SubscriptionId);
            
            // Put this in domain service as this is decision making
            if (subscription.IsEligibleToRecieveAFilm(currentPeriodRentals, currentAllocations))
            {
                Allocation allocation = _allocationRepository.FindBy(message.FilmId);

                if (allocation.StockAvailble())
                {
                    allocation.AllocateUnitTo(message.SubscriptionId); // Create event
                }
            }
        }
    }
}
