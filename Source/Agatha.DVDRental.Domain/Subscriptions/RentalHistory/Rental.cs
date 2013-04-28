using System;

namespace Agatha.DVDRental.Domain.RentalHistory
{
    public class Rental
    {       
        public Rental(int dvdId, int memberId, DateTime sentOut, DateTime returned)
        {
            DvdId = dvdId;
            MemberId = memberId;
            DateSentOut = sentOut;
            DateReturned = returned;
        }

        public int DvdId { get; private set; }
        public int MemberId { get; private set; }
        public DateTime DateSentOut { get; private set; }
        public DateTime DateReturned { get; private set; }
    }
}
