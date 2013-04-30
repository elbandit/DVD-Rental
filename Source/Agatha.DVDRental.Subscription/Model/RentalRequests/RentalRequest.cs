using System;

namespace Agatha.DVDRental.Domain.Subscriptions.RentalRequests
{
    public class RentalRequest
    {      
        public RentalRequest(int filmId, int memberId)
        {
            FilmId = filmId;
            MemberId = memberId;
            Requested = DateTime.Now;
        }

        public string Id { get; private set; }

        public int FilmId { get; private set; }

        public int MemberId { get; private set; }

        public DateTime Requested { get; private set; }

        public string Status { get; set; }      
    }
}
