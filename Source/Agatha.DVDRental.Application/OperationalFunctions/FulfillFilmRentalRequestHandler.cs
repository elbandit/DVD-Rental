using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Domain.Membership;
using Agatha.DVDRental.Domain.RentalLists;

namespace Agatha.DVDRental.Application.OperationalFunctions
{
    public class FulfillFilmRentalRequestHandler
    {
        private IMembershipRepository _membershipRepository;
        private IFilmRepository _filmRepository;

        public FulfillFilmRentalRequestHandler(IFilmRepository filmRepository, IMembershipRepository membershipRepository)
        {
            _filmRepository = filmRepository;
            _membershipRepository = membershipRepository;
        }

        public void Handle(FilmRequested filmRequested) 
        {            
            // rent it to member
            var film = _filmRepository.FindBy(filmRequested.FilmId);

            // 1. Check to see if the member is allowed to rent

            // 2. Are there any copies of the film available?

            //film.RentTo(filmRequested.MemberId);
        }        
    }
}
