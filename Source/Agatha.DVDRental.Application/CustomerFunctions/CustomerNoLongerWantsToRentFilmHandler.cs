using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Application.Commands;
using NServiceBus;

namespace Agatha.DVDRental.Application.CustomerFunctions
{
    public class CustomerNoLongerWantsToRentFilmHandler : IHandleMessages<CustomerNoLongerWantsToRentFilm>
    {
        public void Handle(CustomerNoLongerWantsToRentFilm message)
        {
            Console.Out.WriteLine(@"Member '{0}' has removed the film '{1}' from his list.", message.MemberId, message.FilmId);
        }
    }
}
