using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Domain.RentalLists;
using Agatha.DVDRental.Ui.Application.ApplicationViews;
using AutoMapper;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.Ui.Application
{
    public class Renting
    {
        private readonly IDocumentSession _ravenDbSession;
        private readonly IBus _bus;

        public Renting(IDocumentSession ravenDbSession, IBus bus)
        {
            _ravenDbSession = ravenDbSession;
            _bus = bus;
        }

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

        public void CustomerWantsToRentTheFim(int filmid)
        {
            // quick check for valid command

            _bus.Send();
        }
    }
}
