using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy
{
    public class MarkRentalAllocationsAsDispatchedHandler : IHandleMessages<MarkRentalAllocationsAsDispatched>
    {
        private IFulfilmentRepository _fulfilmentRepository;
        private IBus _bus;

        public MarkRentalAllocationsAsDispatchedHandler(IFulfilmentRepository fulfilmentRepository, IBus bus)
        {
            _fulfilmentRepository = fulfilmentRepository;
            _bus = bus;
        }

        public void Handle(MarkRentalAllocationsAsDispatched message)
         {
             IEnumerable<FulfilmentRequest> assignedRequests = _fulfilmentRepository.FindAllAssignedTo(message.PickerName);

            // Get the DVD update it

             // then event will mark the FulfilmentRequest as dispatched
            // and the event will update the other BC

             using (DomainEvents.Register(HandleEvent()))
             {
                 foreach (FulfilmentRequest request in assignedRequests)
                 {
                     request.Dispatched();
                 }
             }           
         }

         private Action<FulfilmentRequestDispatched> HandleEvent()
         {
             return (FulfilmentRequestDispatched s) => _bus.Publish(new FilmDispatched() { FilmId = s.FilmId, SubscriptionId = s.SubscriptionId }); // See if someone else wants this film
         }
    }
}
