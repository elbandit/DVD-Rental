using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Subscription.Contracts
{
    public class FilmHasBeenAllocated
    {
        public int FilmId { get; set; }

        public int SubscriptionId { get; set; }
    }
}
