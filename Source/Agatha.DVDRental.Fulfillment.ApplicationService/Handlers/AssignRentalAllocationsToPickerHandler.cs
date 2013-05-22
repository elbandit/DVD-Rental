using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Contracts.Commands;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Infrastructure;
using NServiceBus;

namespace Agatha.DVDRental.Fulfillment.ApplicationService.Handlers
{
    public class AssignRentalAllocationsToPickerHandler : IBusinessUseCaseHandler<AssignRentalAllocationsToPicker>
    {
        private IFulfilmentRepository _fulfilmentRepository;
        private IBus _bus;

        public AssignRentalAllocationsToPickerHandler(IFulfilmentRepository fulfilmentRepository, IBus bus)
        {
            _fulfilmentRepository = fulfilmentRepository;
            _bus = bus;
        }

        public void action(AssignRentalAllocationsToPicker businessUseCase)
        {
            IEnumerable<FulfilmentRequest> requestsToAssign = _fulfilmentRepository.FindOldsetUnassignedTop(10);

            using (DomainEvents.Register(HandleEvent()))
            {
                foreach (FulfilmentRequest request in requestsToAssign)
                {
                    request.AssignForPickingTo(businessUseCase.PickerName);
                }
            }   
        }

        private Action<FulfilmentRequestAssignedForPicking> HandleEvent()
        {
            // Need a message forwarder here...
            return (FulfilmentRequestAssignedForPicking s) => _bus.Send(new PublishThatTheFilmIsBeingPicked() { FilmId = s.FilmId, SubscriptionId = s.SubscriptionId }); // See if someone else wants this film
        }
    }
}
