using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Catalogue.Infrastructure;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Subscription.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Subscription.Contracts;
using Agatha.DVDRental.Subscription.Contracts.InternalCommands;
using Agatha.DVDRental.Subscription.Infrastructure;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using NServiceBus;

namespace Agatha.DVDRental.Subscription.ApplicationService.Handlers
{
    public class CustomerWantsToRentAFimHandler : IBusinessUseCaseHandler<CustomerWantsToRentAFim>
    {
        private readonly IBus _bus;
        private readonly RentalRequestRepository _rentalRequestRepository;
        private readonly FilmRepository _filmRepository;

        public CustomerWantsToRentAFimHandler(RentalRequestRepository rentalRequestRepository, FilmRepository filmRepository, IBus bus)
        {
            _rentalRequestRepository = rentalRequestRepository;
            _filmRepository = filmRepository;
            _bus = bus;
        }

        public void action(CustomerWantsToRentAFim businessUseCase)
        {
            Film film = _filmRepository.FindBy(businessUseCase.FilmId);

            using (DomainEvents.Register(AllocateFilm()))
            {
                RentalRequestList rentalRequestList = GetRentalListFor(businessUseCase.SubscriptionId);

                rentalRequestList.CreateRequestFor(film.Id);                
            }
        }

        private Action<FilmRequested> AllocateFilm()
        {
            // Could put a delay in, just in case most customers decide to remove from list within 5 mins
            return (FilmRequested s) => _bus.Send(new AllocateRentalRequest() { FilmId = s.FilmId, SubscriptionId = s.SubscriptionId });
        }

        public RentalRequestList GetRentalListFor(int subscriptionId)
        {
            RentalRequestList rentalRequestList = _rentalRequestRepository.FindBy(subscriptionId);

            if (rentalRequestList == null)
            {
                rentalRequestList = new RentalRequestList(subscriptionId);
                _rentalRequestRepository.Add(rentalRequestList);
            }

            return rentalRequestList;
        }
    }
}
