using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.FulfillmentPolicy
{
    public class AddFilmToStockHandler : IHandleMessages<AddFilmToStock>
    {
        private readonly IDvdRepository _dvdRepository;
        private readonly IDocumentSession _ravenDbSession;        
        private readonly IBus _bus;

        public AddFilmToStockHandler(IDvdRepository dvdRepository, IDocumentSession ravenDbSession, IBus bus)
        {
            _dvdRepository = dvdRepository;
            _ravenDbSession = ravenDbSession;
            _bus = bus;
        }

        public void Handle(AddFilmToStock message)
        {
             using (DomainEvents.Register(HandleEvent()))
            {
                var dvd = new Dvd(message.FilmId, message.Barcode);

                _dvdRepository.Add(dvd);
            }
        }

        private Action<DvdAdded> HandleEvent()
        {
            return (DvdAdded s) =>
                       {
                           _bus.Publish(new FilmAddedToStock() {FilmId = s.FilmId});

                           //_ViewStore.Store(s);
                       };           
        }

    }
}
