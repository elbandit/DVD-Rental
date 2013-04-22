using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.RentalLists
{
    public class RentalRequest
    {      
        public RentalRequest(int filmId, int memberId)
        {
            FilmId = filmId;
            MemberId = memberId;
        }

        public string Id { get; private set; }
        public int FilmId { get; private set; }
        public int MemberId { get; private set; }

        public void fulfilledWith(int dvdId)
        {
            // Store date

        }
    }
}
