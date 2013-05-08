using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Catalogue.Infrastructure;
using Agatha.DVDRental.Domain;
using Agatha.DVDRental.Public.ApplicationService.ApplicationViews;
using Agatha.DVDRental.Subscription.Contracts;
using Agatha.DVDRental.Subscription.Infrastructure;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using Agatha.DVDRental.Subscription.Model.Subscriptions;
using AutoMapper;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.Public.ApplicationService
{
    public class RentingService
    {
        private readonly IDocumentSession _ravenDbSession;
        private readonly IBus _bus;
        private readonly RentalRequestRepository _rentalRequestRepository;
        private readonly FilmRepository _filmRepository;
        private ISubscriptionRepository _subscriptionRepository;

        public RentingService(IDocumentSession ravenDbSession, IBus bus, 
                              RentalRequestRepository rentalRequestRepository, FilmRepository filmRepository, 
                              ISubscriptionRepository subscriptionRepository)
        {
            _ravenDbSession = ravenDbSession;
            _bus = bus;
            _rentalRequestRepository = rentalRequestRepository;
            _filmRepository = filmRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        // Methods are like use cases of the system

        public IEnumerable<FilmView> CustomerWantsToViewFilmsAvailableForRent(string memberEmail)
        {

            var subscription = _subscriptionRepository.FindBy(memberEmail);

            // Find all films
            var all_films = _ravenDbSession.Query<Film>().Take(10).ToList();

            var all_filmviews = Mapper.Map<IEnumerable<Film>, IEnumerable<FilmView>>(all_films);             
     
            // Find all films in rental list
            var all_rentals = _ravenDbSession.Query<RentalRequest>().Where(x => x.MemberId == subscription.Id);

            foreach (var rentalRequest in all_rentals)
            {
                var film = all_filmviews.SingleOrDefault(x => x.Id == rentalRequest.FilmId);

                film.IsOnRentalList = true;
            }

            // Find all films currently being rented
            // _ravenDbSession.Query<Film>();

            return all_filmviews;
        }

        public void CustomerWantsToRentTheFim(int filmid, string memberEmail)
        {
            // quick check for valid command
            var subscription = _subscriptionRepository.FindBy(memberEmail);

            Film film = _filmRepository.FindBy(filmid);

            using (DomainEvents.Register(AllocateFilm()))
            {
                RentalRequestList rentalRequestList = _rentalRequestRepository.FindBy(subscription.Id);

                var request = rentalRequestList.CreateRequestFor(film.Id, subscription.Id); // try and create

                _rentalRequestRepository.Add(request);
                _ravenDbSession.SaveChanges();
            }
        }

        private Action<FilmRequested> AllocateFilm()
        {
            // Could put a delay in, just in case most customers decide to remove from list within 5 mins
            return (FilmRequested s) => _bus.Send(new AllocateRentalRequest() { FilmId = s.FilmId, SubscriptionId = s.SubscriptionId});
        }

        public void CustomerDoesNotWantToRentTheFim(int filmid, string memberEmail)
        {
            var subscription = _subscriptionRepository.FindBy(memberEmail);

            RentalRequestList rentalRequestList = _rentalRequestRepository.FindBy(subscription.Id);

            using (DomainEvents.Register(DeAllocateFilm()))
            {
                var rentalRequest = rentalRequestList.RemoveFromTheList(filmid);

                _ravenDbSession.Delete(rentalRequest);
                _ravenDbSession.SaveChanges(); // Need to hook this up to HttpRequest
            }
        }

        private Action<RentalRequestRemoved> DeAllocateFilm()
        {
            return (RentalRequestRemoved s) => _bus.Send(new DeAllocateRentalRequest()); // See if someone else wants this film
        }

        public IEnumerable<RentalRequestView> ViewRentalListFor(string name)
        {
            var allRequests = _ravenDbSession.Query<RentalRequest>().ToList();

            var allRequestViews = Mapper.Map<IEnumerable<RentalRequest>, IEnumerable<RentalRequestView>>(allRequests);

            return allRequestViews;
        }
    }
}
