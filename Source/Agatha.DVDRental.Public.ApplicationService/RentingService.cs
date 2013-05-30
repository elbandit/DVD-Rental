using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Catalogue.Infrastructure;
using Agatha.DVDRental.Catalogue.Infrastructure.Indexes;
using Agatha.DVDRental.Public.ApplicationService.ApplicationViews;
using Agatha.DVDRental.Public.ApplicationService.Queries;
using Agatha.DVDRental.Subscription.Contracts;
using Agatha.DVDRental.Subscription.Infrastructure;
using Agatha.DVDRental.Subscription.Model.RentalHistory;
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
        private readonly RentalRequestRepository _rentalRequestRepository;       
        private ISubscriptionRepository _subscriptionRepository;

        public RentingService(IDocumentSession ravenDbSession, 
                              RentalRequestRepository rentalRequestRepository,  
                              ISubscriptionRepository subscriptionRepository)
        {
            _ravenDbSession = ravenDbSession;
            _rentalRequestRepository = rentalRequestRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        // Methods are like use cases of the system

        public IEnumerable<FilmView> CustomerWantsToViewFilmsAvailableForRent(string memberEmail)
        {

            //IEnumerable<FilmResult> query = _ravenDbSession
            //    .Query<Film, FilmsUnTyped>()
            //    .Take(100)
            //    .AsProjection<FilmResult>();

            //foreach(FilmResult fr in query)
            //{
            //    Console.WriteLine(fr.Title);
            //}


            //var subscription = _subscriptionRepository.FindBy(memberEmail);

            // Find all films
            var all_films = _ravenDbSession.Query<Film>().Take(10).ToList();

            var all_filmviews = Mapper.Map<IEnumerable<Film>, IEnumerable<FilmView>>(all_films);
         
            // Find all films in rental list
            //RentalRequestList rentalList = GetRentalListFor(subscription.Id);

            //if (rentalList != null)
            //    foreach (var rentalRequest in rentalList.RentalRequests)
            //    {
            //        var film = all_filmviews.SingleOrDefault(x => x.Id == rentalRequest.FilmId);

            //        film.IsOnRentalList = true;
            //    }
            
            return all_filmviews;
        }

        public IEnumerable<RentalRequestView> ViewRentalListFor(string memberEmail)
        {
            var subscription = _subscriptionRepository.FindBy(memberEmail);

            IEnumerable<RentalRequestView> allRequestViews = _ravenDbSession
                                                        .Query<RentalRequestView, RentalRequestIndex>()
                                                        .Take(100)
                                                        .Where(x => x.SubscriptionId == subscription.Id)
                                                        .AsProjection<RentalRequestView>();            
            return allRequestViews.ToList();
        }

        
        private RentalRequestList GetRentalListFor(int subscriptionId)
        {
            RentalRequestList rentalRequestList = _rentalRequestRepository.FindBy(subscriptionId);

            if (rentalRequestList == null)
            {
                rentalRequestList = new RentalRequestList(subscriptionId);
                _rentalRequestRepository.Add(rentalRequestList);
            }

            return rentalRequestList;
        }

        public IEnumerable<Rental> GetRentalHistoryFor(string memberEmail)
        {
            var subscription = _subscriptionRepository.FindBy(memberEmail);

            IEnumerable<Rental> rentalHistory = _ravenDbSession
                .Query<Rental>()
                .Take(100)
                .Where(x => x.SubscriptionId == subscription.Id);

            return rentalHistory;
        }
    }
}
