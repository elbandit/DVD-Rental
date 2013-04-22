using System.Collections;
using System.Collections.Generic;
using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Domain.Membership;
using System.Linq;

namespace Agatha.DVDRental.Domain.RentalLists
{
    public class RentalList
    {
        private IList<RentalRequest> _rentalRequests;

        public RentalList(IList<RentalRequest> rentalRequests)
        {
            _rentalRequests = rentalRequests;
        }

        public void organise()
        {

        
    
        }
        
        public RentalRequest CreateRequestFor(Film film, Member member)
        {
            // make sure we don't already have this in the list

            // does this list have an age restriction?

            // give it a priority order
            var request = new RentalRequest(film.Id, member.Id);

            DomainEvents.Raise(new FilmRequested(film.Id, member.Id));

            return request;
        }

        public RentalRequest RemoveFromTheList(int filmId)
        {
            return _rentalRequests.SingleOrDefault(x => x.FilmId == filmId);         
        } 
    }
}
