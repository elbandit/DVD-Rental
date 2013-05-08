using System;

namespace Agatha.DVDRental.Fulfillment.Model.Stock
{
    public class CurrentLoan
    {       
        public CurrentLoan(int subscriptionId, DateTime dateLoanedOut)
        {
            SubscriptionId = subscriptionId;
            DateLoanedOut = dateLoanedOut;
        }

        public int SubscriptionId { get; private set; }
        public DateTime DateLoanedOut { get; private set; }
    }
}
