using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Fulfillment.Contracts.Commands
{
    public class PublishThatAFilmHasBeenDispatched
    {
        public int FilmId { get; set; }

        public int SubscriptionId { get; set; }
    }
}
