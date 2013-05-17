using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Subscription.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Subscription.Contracts;
using Agatha.DVDRental.Subscription.Contracts.InternalCommands;
using Agatha.DVDRental.Subscription.Infrastructure;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using NServiceBus;

namespace Agatha.DVDRental.Subscription.ApplicationService.Handlers
{
    public class CustomerIsNotInterestedInRentingThisFimHandler : IBusinessUseCaseHandler<CustomerIsNotInterestedInRentingThisFim>
    {
        private readonly IBus _bus;
        private readonly RentalRequestRepository _rentalRequestRepository;

        public CustomerIsNotInterestedInRentingThisFimHandler(RentalRequestRepository rentalRequestRepository, IBus bus)
        {
            _rentalRequestRepository = rentalRequestRepository;
            _bus = bus;
        }

        public void action(CustomerIsNotInterestedInRentingThisFim businessUseCase)
        {
            RentalRequestList rentalRequestList = _rentalRequestRepository.FindBy(businessUseCase.SubscriptionId);

            using (DomainEvents.Register(DeAllocateFilm()))
            {
                rentalRequestList.RemoveFromTheList(businessUseCase.FilmId);
            }
        }

        private Action<RentalRequestRemoved> DeAllocateFilm()
        {
            return (RentalRequestRemoved s) => _bus.Send(new DeAllocateRentalRequest()); // See if someone else wants this film
        }
    }
}
