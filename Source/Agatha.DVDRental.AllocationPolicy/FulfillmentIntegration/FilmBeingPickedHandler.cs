using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy.FulfillmentIntegration
{
    public class FilmBeingPickedHandler : IHandleMessages<FilmBeingPicked>
    {
        private IRentalRequestRepository _rentalRequestRepository;

        public FilmBeingPickedHandler(IRentalRequestRepository rentalRequestRepository)
        {
            _rentalRequestRepository = rentalRequestRepository;
        }

        public void Handle(FilmBeingPicked message)
        {
            RentalRequest request = _rentalRequestRepository.FindBy(message.SubscriptionId, message.FilmId);

            if (request != null)
                request.IsReadyForDispatch();
        }
    }
}
