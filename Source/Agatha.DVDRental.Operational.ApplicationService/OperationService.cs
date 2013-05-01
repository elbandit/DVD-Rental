using System;
using Agatha.DVDRental.Catalogue.Catalogue;
using Raven.Client;

namespace Agatha.DVDRental.Operational.ApplicationService
{
    public class OperationService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IDocumentSession _ravenDbSession;

        public OperationService(IFilmRepository filmRepository, IDocumentSession ravenDbSession)
        {
            _filmRepository = filmRepository;
            _ravenDbSession = ravenDbSession;
        }

        // Methods are like use cases of the system

        public void OperatorWantsToAddStock(int filmId, string barcode  )
        {

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
    }
}
