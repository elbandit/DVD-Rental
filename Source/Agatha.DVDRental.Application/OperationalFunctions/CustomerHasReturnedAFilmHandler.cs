using System;
using Agatha.DVDRental.Domain;
using Agatha.DVDRental.Domain.Stock;
using Agatha.DVDRental.Messages.CustomerScenarios.Commands;
using Agatha.DVDRental.Messages.CustomerScenarios.Events;
using Agatha.DVDRental.Messages.OperationalScenarios.Events;
using NServiceBus;
using Raven.Client;


namespace Agatha.DVDRental.Application.OperationalFunctions
{
    public class CustomerHasReturnedAFilmHandler : IHandleMessages<ReturnAFilm>
    {
        private IDvdRepository _dvdRepository;
        private readonly IDocumentSession _ravenDbSession;
        private readonly IBus _bus;

        public CustomerHasReturnedAFilmHandler(IDvdRepository dvdRepository,
                                               IDocumentSession ravenDbSession,
                                               IBus bus)
        {
            _dvdRepository = dvdRepository;
            _ravenDbSession = ravenDbSession;
            _bus = bus;
        }

        public void Handle(ReturnAFilm message)
        {
            using (DomainEvents.Register(CallbackToHandleDomainEvent()))
            {
                var dvd = _dvdRepository.FindBy(message.DvdId);

                dvd.ReturnLoan();                                
            }

            _ravenDbSession.SaveChanges(); // NServiceBus will promote this to a distributed transaction.    
        }

        private Action<DvdReturned> CallbackToHandleDomainEvent()
        {
            return (DvdReturned s) => _bus.Publish(new DvdReturnedMessage());
        }
    }
}
