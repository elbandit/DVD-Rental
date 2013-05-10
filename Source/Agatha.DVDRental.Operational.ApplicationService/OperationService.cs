using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.Operational.ApplicationService
{
    public class OperationService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IDocumentSession _ravenDbSession;        
        private readonly IBus _bus;

        public OperationService(IFilmRepository filmRepository, 
                                IDocumentSession ravenDbSession, 
                                IBus bus)
        {
            _filmRepository = filmRepository;
            _ravenDbSession = ravenDbSession;
            _bus = bus;
        }

        // Methods are like use cases of the system

        public void OperatorWantsToAddStock(int filmId, string barcode)
        {            
            _bus.Send(new AddFilmToStock() { FilmId = filmId, Barcode = barcode });
        }

        public void OperatorWantsToProceesAFilmReturn(string barcode)
        {

        }

        public void OperatorWantsToPickRentalAllocations(string processorName)
        {
            _bus.Send(new AssignRentalAllocations() { PickerName = processorName });
        }

        public string OperatorWantsToViewAssignedRentalAllocations(string processorName)
        {
            return "";
        }

        public void OperatorWantsToMarkRentalAllocationsAsDispatched(string processorName)
        {
            _bus.Send(new MarkRentalAllocationsAsDispatched() { PickerName = processorName });
        }

        public void AddFilmToCatalogue(string title)
        {
            var film = new Film(DateTime.Now, title);

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

        public IEnumerable<FulfilmentRequest> ViewAllFulfilmentRequests()
        {
            return _ravenDbSession.Query<FulfilmentRequest>().Take(100).ToList();
        }
    }
}
