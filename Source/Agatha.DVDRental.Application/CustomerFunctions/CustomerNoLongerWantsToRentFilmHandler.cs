using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Application.Commands;
using Agatha.DVDRental.Domain;
using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Domain.Membership;
using Agatha.DVDRental.Domain.RentalLists;
using Agatha.DVDRental.Messages.CustomerScenarios.Commands;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.Application.CustomerFunctions
{
    public class CustomerNoLongerWantsToRentFilmHandler : IHandleMessages<CustomerNoLongerWantsToRentFilm>
    {
        private readonly IRentalListRepository _rentalListRepository;
        private readonly IDocumentSession _ravenDbSession;

        public CustomerNoLongerWantsToRentFilmHandler(IRentalListRepository rentalListRepository, 
                                                      IDocumentSession ravenDbSession)
        {            
            _rentalListRepository = rentalListRepository;
            _ravenDbSession = ravenDbSession; // Acts as the unit of work
        }

        public void Handle(CustomerNoLongerWantsToRentFilm message)
        {                       
            RentalList rentalList = _rentalListRepository.FindBy(message.MemberId);

            var rentalRequest = rentalList.RemoveFromTheList(message.FilmId);
                      
            _ravenDbSession.Delete(rentalRequest);
            _ravenDbSession.SaveChanges(); // NServiceBus will promote this to a distributed transaction. 

            Console.Out.WriteLine(@"Member '{0}' has removed the film '{1}' from his list.", message.MemberId, message.FilmId);
        }
    }
}
