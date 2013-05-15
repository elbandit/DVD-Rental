using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy
{
    public class MarkRentalAllocationsAsDispatchedHandler : IHandleMessages<FulfilLoan>
    {
        private IFulfilmentRepository _fulfilmentRepository;
        private IDvdRepository _dvdRepository;
        private IBus _bus;

        public MarkRentalAllocationsAsDispatchedHandler(IFulfilmentRepository fulfilmentRepository, IBus bus, 
                                                        IDvdRepository dvdRepository)
        {
            _fulfilmentRepository = fulfilmentRepository;
            _bus = bus;
            _dvdRepository = dvdRepository;
        }

        public void Handle(FulfilLoan message)
        {
            var request = _fulfilmentRepository.FindBy(message.FulfilmentRequestId);        

            var dvd = _dvdRepository.FindBy(message.DvdId);

             using (DomainEvents.Register(HandleEvent()))
             {               
                request.FulfilledWith(dvd.Id);                  
             }           
         }

         private Action<FulfilmentRequestDispatched> HandleEvent()
         {
             return (FulfilmentRequestDispatched s) =>
                        {
                            _bus.Publish(new FilmDispatched() {FilmId = s.FilmId, SubscriptionId = s.SubscriptionId});

                            _bus.Send(new AssignDvdToSubscription() { DvdId = s.DvdId, SubscriptionId = s.SubscriptionId });
                        };
             
         }
    }
}
