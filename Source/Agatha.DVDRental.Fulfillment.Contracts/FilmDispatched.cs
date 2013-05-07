using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Fulfillment.Contracts
{
    public class FilmDispatched
    {
        public int FilmId { get; set; }

        public int SubscriptionId { get; set; }
    }
}
