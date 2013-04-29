using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.ApplicationService.ApplicationViews;
using Agatha.DVDRental.Domain;
using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Domain.RentalLists;
using Agatha.DVDRental.Domain.Subscriptions.RentalRequests;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Messages.OperationalScenarios.Commands;
using AutoMapper;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.ApplicationService
{
    public class RentingService
    {
        private readonly IDocumentSession _ravenDbSession;
        private readonly IBus _bus;
        private readonly RentalRequestRepository _rentalRequestRepository;
        private readonly FilmRepository _filmRepository;

        public RentingService(IDocumentSession ravenDbSession, IBus bus, 
                       RentalRequestRepository rentalRequestRepository, FilmRepository filmRepository)
        {
            _ravenDbSession = ravenDbSession;
            _bus = bus;
            _rentalRequestRepository = rentalRequestRepository;
            _filmRepository = filmRepository;
        }

        // Methods are like use cases of the system

        public IEnumerable<FilmView> CustomerWantsToViewFilmsAvailableForRent(int memberId)
        {      
            // Find all films
            var all_films = _ravenDbSession.Query<Film>().Take(10).ToList();

            var all_filmviews = Mapper.Map<IEnumerable<Film>, IEnumerable<FilmView>>(all_films);             
     
            // Find all films in rental list
            var all_rentals = _ravenDbSession.Query<RentalRequest>().Where(x => x.MemberId == memberId);

            foreach (var rentalRequest in all_rentals)
            {
                var film = all_filmviews.SingleOrDefault(x => x.Id == rentalRequest.FilmId);

                film.IsOnRentalList = true;
            }

            // Find all films currently being rented
            // _ravenDbSession.Query<Film>();

            return all_filmviews;
        }

        public void CustomerWantsToRentTheFim(int filmid, int memberId)
        {
            // quick check for valid command

            Film film = _filmRepository.FindBy(filmid);

            using (DomainEvents.Register(AllocateFilm()))
            {
                RentalRequestList rentalRequestList = _rentalRequestRepository.FindBy(memberId);

                var request = rentalRequestList.CreateRequestFor(film, memberId); // try and create

                _rentalRequestRepository.Add(request);
                _ravenDbSession.SaveChanges();
            }
        }

        private Action<FilmRequested> AllocateFilm()
        {
            return (FilmRequested s) => _bus.Send(new AllocateRentalRequest());
        }

        public void CustomerDoesNotWantToRentTheFim(int filmid, int memberId)
        {
            RentalRequestList rentalRequestList = _rentalRequestRepository.FindBy(memberId);

            using (DomainEvents.Register(DeAllocateFilm()))
            {
                var rentalRequest = rentalRequestList.RemoveFromTheList(filmid);

                _ravenDbSession.Delete(rentalRequest);
                _ravenDbSession.SaveChanges(); // Need to hook this up to HttpRequest           
            }
        }

        private Action<RentalRequestRemoved> DeAllocateFilm()
        {
            return (RentalRequestRemoved s) => _bus.Send(new AllocateRentalRequest()); // See if someone else wants this film
        }
    }
}
