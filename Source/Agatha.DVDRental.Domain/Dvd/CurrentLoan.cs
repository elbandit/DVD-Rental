using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.Dvd
{
    public class CurrentLoan
    {
        private readonly int _member;
        private readonly DateTime _dateLoanedOut;

        public CurrentLoan(int member, DateTime dateLoanedOut)
        {
            _member = member;
            _dateLoanedOut = dateLoanedOut;
        }
    }
}
