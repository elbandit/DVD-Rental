using System;
using Agatha.DVDRental.Fulfillment.Infrastructure;

namespace Agatha.DVDRental.Fulfillment.Model.Stock
{
    public class Dvd
    {
        public int Id { get; private set; }
        public int FilmId { get; private set; }
        public CurrentLoan CurrentLoan { get; private set; }

        public void LoanTo(int memberId, DeliveryAddress address)
        {
            CurrentLoan = new CurrentLoan(memberId, DateTime.Now);

            DomainEvents.Raise(new FilmLoanedToMember(FilmId, memberId));  // Needs to be removed from the rental list            
        }

        public void ReturnLoan()
        {
            CurrentLoan = null;

            DomainEvents.Raise(new FilmReturned()); // Needs to update the rental history list
        }
    }
}
