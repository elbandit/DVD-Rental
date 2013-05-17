using System;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Subscription.Contracts.InternalCommands;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy.FulfillmentIntegration
{
    public class FilmDispatchedHandler : IHandleMessages<FilmDispatched>
    {
        private IRentalRequestRepository _rentalRequestRepository;
        private IBus _bus;

        public FilmDispatchedHandler(IRentalRequestRepository rentalRequestRepository, IBus bus)
        {
            _rentalRequestRepository = rentalRequestRepository;
            _bus = bus;
        }

        public void Handle(FilmDispatched message)
        {
            var rentalRequestlist = _rentalRequestRepository.FindBy(message.SubscriptionId);

            using (DomainEvents.Register(HandleEvent()))
            {
                rentalRequestlist.Fulfilled(message.FilmId);
            }            
        }

        private Action<RequestFulfilled> HandleEvent()
        {
            return (RequestFulfilled s) =>
            {                
                // Should add film name
                _bus.Send(new AddRentalHistory() { FilmId = s.FilmId, SubscriptionId = s.SubscriptionId });
            };

        }        
    }
}
