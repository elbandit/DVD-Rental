using NServiceBus;

namespace Agatha.DVDRental.Subscription.Contracts
{
    public class DeAllocateRentalRequest : ICommand
    {
        public int SubscriptionId { get; set; }

        public int FilmId { get; set; }
    }
}