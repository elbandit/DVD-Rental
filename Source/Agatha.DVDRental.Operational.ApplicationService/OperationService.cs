using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.Operational.ApplicationService
{
    public class OperationService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IDocumentSession _ravenDbSession;
        private readonly IDvdRepository _dvdRepository;
        private readonly IBus _bus;

        public OperationService(IFilmRepository filmRepository, 
                                IDocumentSession ravenDbSession, 
                                IDvdRepository dvdRepository, IBus bus)
        {
            _filmRepository = filmRepository;
            _ravenDbSession = ravenDbSession;
            _dvdRepository = dvdRepository;
            _bus = bus;
        }

        // Methods are like use cases of the system

        public void OperatorWantsToAddStock(int filmId, string barcode  )
        {           
            using (DomainEvents.Register(HandleEvent()))
            {
                var dvd = new Dvd(filmId);

                _dvdRepository.Add(dvd);

                _ravenDbSession.SaveChanges();
            }
        }

        private Action<DvdAdded> HandleEvent()
        {
            return (DvdAdded s) => _bus.Publish(new FilmAddedToStock() {FilmId = s.FilmId}); // See if someone else wants this film
        }

        public void OperatorWantsToProceesAFilmReturn(string barcode)
        {

        }

        public void OperatorWantsToPickRentalAllocations(string processorName)
        {

        }

        public string OperatorWantsToViewAssignedRentalAllocations(string processorName)
        {
            return "";
        }

        public void OperatorWantsToMarkRentalAllocationsAsDispatched(string processorName)
        {

        }

        public void AddFilmToCatalogue(string title)
        {
            var film = new Film(DateTime.Now);

            _filmRepository.Add(film);

            _ravenDbSession.SaveChanges();
        }

        public IEnumerable<Film> ViewAllFilms()
        {
            return _ravenDbSession.Query<Film>().Take(10).ToList();
        }

        public IEnumerable<Dvd> ViewStockFor(int filmId)
        {
            return _ravenDbSession.Query<Dvd>().Where(x => x.FilmId == filmId).ToList();
        }
    }
}
