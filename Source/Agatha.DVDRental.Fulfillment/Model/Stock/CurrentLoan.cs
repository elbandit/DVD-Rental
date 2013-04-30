using System;

namespace Agatha.DVDRental.Fulfillment.Model.Stock
{
    public class CurrentLoan
    {       
        public CurrentLoan(int member, DateTime dateLoanedOut)
        {
            MemberId = member;
            DateLoanedOut = dateLoanedOut;
        }

        public int MemberId { get; private set; }
        public DateTime DateLoanedOut { get; private set; }
    }
}
