using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Fulfillment.Contracts.Commands
{
    public class PublishThatACopyOfAFilmHasBeenAddedToTheStock
    {
        public int FilmId { get; set; }
    }
}
