using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace Agatha.DVDRental.Application.Commands
{
    public class CustomerNoLongerWantsToRentFilm : ICommand
    {
        public int FilmId { get; set; }
        public int MemberId { get; set; }
    }
}
