using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Fulfillment.Contracts
{
    public class AssignDvdToSubscription
    {
        public int SubscriptionId { get; set; }

        public int DvdId { get; set; }
    }
}
