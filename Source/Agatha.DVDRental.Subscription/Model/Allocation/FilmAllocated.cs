using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Subscription.Model.Allocation
{
    public class FilmAllocated
    {
        public int FilmId { get; set; }
        public int SubscriptionId { get; set; }        
    }
}
