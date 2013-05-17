using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using Agatha.DVDRental.Infrastructure;
using NServiceBus;
using FilmReturned = Agatha.DVDRental.Fulfillment.Contracts.FilmReturned;

namespace Agatha.DVDRental.Fulfillment.ApplicationService.Handlers
{
    public class ReturnAFilmHandler : IBusinessUseCaseHandler<ReturnAFilm>
    {
        private IDvdRepository _dvdRepository;
        private readonly IBus _bus;

        public ReturnAFilmHandler(IDvdRepository dvdRepository, IBus bus)
        {
            _dvdRepository = dvdRepository;
            _bus = bus;
        }

        public void action(ReturnAFilm businessUseCase)
        {
            using (DomainEvents.Register(CallbackToHandleDomainEvent()))
            {
                var dvd = _dvdRepository.FindBy(businessUseCase.DvdId);

                dvd.ReturnLoan();
            } 
        }

        private Action<DvdReturned> CallbackToHandleDomainEvent()
        {
            return (DvdReturned s) => _bus.Publish(new FilmReturned());
        }
    }
}
