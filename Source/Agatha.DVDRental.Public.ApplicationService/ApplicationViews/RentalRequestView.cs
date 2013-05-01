using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Public.ApplicationService.ApplicationViews
{
    public class RentalRequestView
    {
        public string Id { get; private set; }

        public int FilmId { get; private set; }

        public int MemberId { get; private set; }

        public DateTime Requested { get; private set; }

        public bool CanBeRemovedFromList { get; set; }
    }
}
