using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using Agatha.DVDRental.Operational.ApplicationService.ApplicationViews;
using NServiceBus;
using Raven.Client;

namespace Agatha.DVDRental.Operational.ApplicationService
{
    public class OperationService
    {      
        private readonly IDocumentSession _ravenDbSession;        

        public OperationService(IDocumentSession ravenDbSession)
        {           
            _ravenDbSession = ravenDbSession;
        }        

        public PickListView OperatorWantsToViewAssignedRentalAllocations(string processorName)
        {
            var fulfilmentRequests = _ravenDbSession.Query<FulfilmentRequest>().Where(x => x.AssignedTo == processorName).ToList();

            var pickListView = new PickListView();

            pickListView.PickRequests = new List<PickRequestView>();
            pickListView.AssignedTo = processorName;

            foreach(FulfilmentRequest request in fulfilmentRequests)
            {
                PickRequestView pickRequestView = new PickRequestView();
                
                pickRequestView.FilmTitle = _ravenDbSession.Load<Film>(request.FilmId).Title;
                pickRequestView.DvdIdsToFulfil = new List<int>();
                pickRequestView.FulfilmentRequestId = request.Id;

                foreach (Dvd dvd in _ravenDbSession.Query<Dvd>().Where(x => x.CurrentLoan == null && x.FilmId == request.FilmId).ToList())
                {
                    pickRequestView.DvdIdsToFulfil.Add(dvd.Id);
                }

                pickListView.PickRequests.Add(pickRequestView);
            }

            return pickListView;
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
