using System;
using Agatha.DVDRental.Infrastructure;

namespace Agatha.DVDRental.Fulfillment.Model.Stock
{
    public class Dvd
    {
        public Dvd(int filmId, string barcode)
        {
            this.FilmId = filmId;
            Barcode = barcode;

            DomainEvents.Raise(new DvdAdded() {FilmId = filmId});
        }

        public int Id { get; private set; }
        public int FilmId { get; private set; }
        public string Barcode { get; private set; }
        public CurrentLoan CurrentLoan { get; private set; }

        public void LoanTo(int subscriptionId)
        {
            CurrentLoan = new CurrentLoan(subscriptionId, DateTime.Now);

            DomainEvents.Raise(new FilmLoanedOut() {FilmId = FilmId, SubscriptionId = subscriptionId});  // Needs to be removed from the rental list            
        }

        public void ReturnLoan()
        {           
            if (CurrentLoan != null)
            {
                DomainEvents.Raise(new FilmReturned() { FilmId = FilmId, SubscriptionId = CurrentLoan.SubscriptionId }); // Needs to update the rental history list

                CurrentLoan = null;
            }            
        }
    }
}
