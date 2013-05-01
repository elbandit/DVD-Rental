using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using Raven.Client;

namespace Agatha.DVDRental.Operational.ApplicationService
{
    public class OperationService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IDocumentSession _ravenDbSession;
        private readonly IDvdRepository _dvdRepository;

        public OperationService(IFilmRepository filmRepository, 
                                IDocumentSession ravenDbSession, IDvdRepository dvdRepository)
        {
            _filmRepository = filmRepository;
            _ravenDbSession = ravenDbSession;
            _dvdRepository = dvdRepository;
        }

        // Methods are like use cases of the system

        public void OperatorWantsToAddStock(int filmId, string barcode  )
        {
            var dvd = new Dvd(filmId);           

            _dvdRepository.Add(dvd);

            _ravenDbSession.SaveChanges();
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
