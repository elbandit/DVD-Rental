using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using Agatha.DVDRental.Infrastructure;
using NServiceBus;

namespace Agatha.DVDRental.Fulfillment.ApplicationService.Handlers
{
    public class AddStockHandler : IBusinessUseCaseHandler<AddStock>
    {
        private readonly IDvdRepository _dvdRepository;
        private readonly IBus _bus;

        public void action(AddStock businessUseCase)
        {
            using (DomainEvents.Register(HandleEvent()))
            {
                var dvd = new Dvd(businessUseCase.FilmId, businessUseCase.Barcode);

                _dvdRepository.Add(dvd);
            }
        }

        private Action<DvdAdded> HandleEvent()
        {
            return (DvdAdded s) =>
            {
                _bus.Publish(new FilmAddedToStock() { FilmId = s.FilmId });              
            };
        }
    }
}
