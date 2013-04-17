using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Domain.Membership;

namespace Agatha.DVDRental.Domain.Dvd
{
    public class Dvd
    {
        private int _id { get; set; }
        private int _filmId { get; set; }
        private CurrentLoan _currentLoan { get; set; }

        public void LoanTo(int memberId)
        {
            _currentLoan = new CurrentLoan(memberId, DateTime.Now);

            DomainEvents.Raise(new FilmLoanedToMember(_filmId, memberId));  // Needs to remove from rental list            
        }

        public void ReturnLoan()
        {
            _currentLoan = null;

            DomainEvents.Raise(new FilmReturned()); // Needs to remove from rental list
        }
    }
}
