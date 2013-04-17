using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.RentalLists
{
    public class FilmRequested
    {
        public FilmRequested(int filmId, int memberId)
        {
            FilmId = filmId;
            MemberId = memberId;
        }

        public int FilmId { get; set; }

        public int MemberId { get; set; }
    }
}
