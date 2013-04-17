using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.RentalLists
{
    public class RentalRequest
    {
        public string Id { get; private set; }
        public int _filmId { get; private set; }
        public int _memberId { get; private set; }

        public RentalRequest(int filmId, int memberId)
        {
            _filmId = filmId;
            _memberId = memberId;
        }

        public void fulfilledWith(int dvdId)
        {
            // Store date

        }
    }
}
