using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy.FulfillmentIntegration
{
    public class FilmDispatchedHandler : IHandleMessages<FilmDispatched>
    {
        private IRentalRequestRepository _rentalRequestRepository;

        public FilmDispatchedHandler(IRentalRequestRepository rentalRequestRepository)
        {
            _rentalRequestRepository = rentalRequestRepository;
        }

        public void Handle(FilmDispatched message)
        {
            var rentalRequestlist = _rentalRequestRepository.FindBy(message.SubscriptionId);

            rentalRequestlist.Fulfilled(message.FilmId);

            // Send email to customer...
        }
    }
}
