using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy
{
    public class AssignRentalAllocationsHandler : IHandleMessages<AssignRentalAllocations>
    {
        private IFulfilmentRepository _fulfilmentRepository;
        private IBus _bus;

        public AssignRentalAllocationsHandler(IFulfilmentRepository fulfilmentRepository, IBus bus)
        {
            _fulfilmentRepository = fulfilmentRepository;
            _bus = bus;
        }

        public void Handle(AssignRentalAllocations message)
        {
            IEnumerable<FulfilmentRequest> requestsToAssign = _fulfilmentRepository.FindOldsetUnassignedTop(10);

            using (DomainEvents.Register(HandleEvent()))
            {
                foreach (FulfilmentRequest request in requestsToAssign)
                {
                    request.AssignForPickingTo(message.PickerName);
                }
            }    
        }

        private Action<FulfilmentRequestAssignedForPicking> HandleEvent()
        {
            return (FulfilmentRequestAssignedForPicking s) => _bus.Publish(new FilmBeingPicked() { FilmId = s.FilmId, SubscriptionId = s.SubscriptionId }); // See if someone else wants this film
        }
    }
}
