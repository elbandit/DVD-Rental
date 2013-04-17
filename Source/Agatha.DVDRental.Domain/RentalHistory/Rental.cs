using System;

namespace Agatha.DVDRental.Domain.RentalHistory
{
    public class Rental
    {
        private int _dvdId;
        private int _memberId;
        private readonly DateTime _sentOut;
        private readonly DateTime _returned;

        public Rental(int dvdId, int memberId, DateTime sentOut, DateTime returned)
        {
            _dvdId = dvdId;
            _memberId = memberId;
            _sentOut = sentOut;
            _returned = returned;
        }
    }
}
