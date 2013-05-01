using System;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.FulfillmentPolicy
{
    public class ReturnFilmHandler : IHandleMessages<ReturnFilm>
    {
        private IDvdRepository _dvdRepository;
        private readonly IDocumentSession _ravenDbSession;
        private readonly IBus _bus;

        public ReturnFilmHandler(IDvdRepository dvdRepository,
                                               IDocumentSession ravenDbSession,
                                               IBus bus)
        {
            _dvdRepository = dvdRepository;
            _ravenDbSession = ravenDbSession;
            _bus = bus;
        }

        public void Handle(ReturnFilm message)
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
            return (DvdReturned s) => _bus.Publish(new FilmReturned());
        }
    }
}
