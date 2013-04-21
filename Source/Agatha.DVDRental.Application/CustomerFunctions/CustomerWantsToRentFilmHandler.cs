using System;
using System.Collections.Generic;
using Agatha.DVDRental.Application.Commands;
using Agatha.DVDRental.Domain;
using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Domain.Membership;
using Agatha.DVDRental.Domain.RentalLists;
using Agatha.DVDRental.Messages.CustomerScenarios;
using Agatha.DVDRental.Messages.CustomerScenarios.Commands;
using Agatha.DVDRental.Messages.CustomerScenarios.Events;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.Application.CustomerFunctions
{
    public class CustomerWantsToRentFilmHandler : IHandleMessages<CustomerWantsToRentFilm>
    {
        private readonly IRentalListRepository _rentalListRepository;
        private readonly IDocumentSession _ravenDbSession;
        private readonly IBus _bus;
        private readonly IFilmRepository _filmRepository;

        public CustomerWantsToRentFilmHandler(IBus bus, 
                                    IFilmRepository filmRepository, 
                                    IRentalListRepository rentalListRepository, 
                                    IDocumentSession ravenDbSession)
        {
            _bus = bus;
            _filmRepository = filmRepository;
            _rentalListRepository = rentalListRepository;
            _ravenDbSession = ravenDbSession; // Acts as the unit of work
        }

        public void Handle(CustomerWantsToRentFilm message)
        {                        
            using (DomainEvents.Register(CallbackToHandleDomainEvent()))
            {
                //Film film = _filmRepository.FindBy(message.FilmId);            

                var film = new Film(message.FilmId, DateTime.Now);

                RentalList rentalList = _rentalListRepository.FindBy(message.MemberId);

                var request = rentalList.CreateRequestFor(film, new Member(message.MemberId));

                _rentalListRepository.Add(request);

                //throw new ApplicationException("Test");

                Console.Out.WriteLine(@"Member '{0}' has added film '{1}' to his list. {2}", message.MemberId, message.FilmId, _filmRepository.SayHello());
            }

            _ravenDbSession.SaveChanges(); // NServiceBus will promote this to a distributed transaction.    
        }

        private Action<FilmRequested> CallbackToHandleDomainEvent()
        {
            return (FilmRequested s) => _bus.Publish(new FilmRequestedMessage() { FilmId = s.FilmId, MemberId = s.MemberId });
        }
    }  
}
