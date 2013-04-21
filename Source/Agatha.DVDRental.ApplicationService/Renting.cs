using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.ApplicationService.ApplicationViews;
using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Domain.RentalLists;
using AutoMapper;
using Raven.Client;

namespace Agatha.DVDRental.ApplicationService
{
    public class Renting
    {
        private readonly IDocumentSession _ravenDbSession;

        public Renting(IDocumentSession ravenDbSession)
        {
            _ravenDbSession = ravenDbSession;
        }

        public IEnumerable<FilmView> CustomerWantsToViewFilmsAvailableForRent(int memberId)
        {      
            // Find all films
            var all_films = _ravenDbSession.Query<Film>().Take(10).ToList();

            var all_filmviews = Mapper.Map<IEnumerable<Film>, IEnumerable<FilmView>>(all_films);             
     
            // Find all films in rental list
            var all_rentals = _ravenDbSession.Query<RentalRequest>().Where(x => x._memberId == memberId);

            foreach (var rentalRequest in all_rentals)
            {
                var film = all_filmviews.SingleOrDefault(x => x.Id == rentalRequest._filmId);

                film.IsOnRentalList = true;
            }

            // Find all films currently being rented
            // _ravenDbSession.Query<Film>();

            return all_filmviews;
        }
    }
}
