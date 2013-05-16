using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using Agatha.DVDRental.Infrastructure;
using NServiceBus;

namespace Agatha.DVDRental.Fulfillment.ApplicationService.Handlers
{
    public class MarkRentalAllocationAsDispatchedHandler : IBusinessUseCaseHandler<MarkRentalAllocationAsDispatched>
    {
        private IFulfilmentRepository _fulfilmentRepository;
        private IDvdRepository _dvdRepository;
        private IBus _bus;

        public void action(MarkRentalAllocationAsDispatched businessUseCase)
        {
            var request = _fulfilmentRepository.FindBy(businessUseCase.FulfilmentRequestId);

            var dvd = _dvdRepository.FindBy(businessUseCase.DvdId);

            using (DomainEvents.Register(HandleEvent()))
            {
                request.FulfilledWith(dvd.Id);
            }     
        }

        private Action<FulfilmentRequestDispatched> HandleEvent()
        {
            return (FulfilmentRequestDispatched s) =>
            {
                _bus.Publish(new FilmDispatched() { FilmId = s.FilmId, SubscriptionId = s.SubscriptionId });

                _bus.Send(new AssignDvdToSubscription() { DvdId = s.DvdId, SubscriptionId = s.SubscriptionId });
            };

        }
    }
}
