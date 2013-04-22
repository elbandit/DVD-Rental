using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.Dvd
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
