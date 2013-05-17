using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Subscription.Contracts;
using Agatha.DVDRental.Subscription.Contracts.Events;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy.SubscriptionIntegration
{
    public class FilmHasBeenAllocatedHandler : IHandleMessages<FilmHasBeenAllocated>
    {
        private IFulfilmentRepository _fulfilmentRepository;

        public FilmHasBeenAllocatedHandler(IFulfilmentRepository fulfilmentRepository)
        {
            _fulfilmentRepository = fulfilmentRepository;
        }

        public void Handle(FilmHasBeenAllocated message)
        {            
            var request = _fulfilmentRepository.FindBy(message.FilmId, message.SubscriptionId);
          
            if (request == null)
            {
                var fulfilmentRequest = new FulfilmentRequest(message.FilmId, message.SubscriptionId);
                           
                _fulfilmentRepository.Add(fulfilmentRequest);
            }
        }
    }
}
 