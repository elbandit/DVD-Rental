using System;

namespace Agatha.DVDRental.Subscription.Contracts.InternalCommands
{
    public class AddRentalHistory
    {
        public int FilmId { get; set; }

        public int SubscriptionId { get; set; }

        public DateTime SentOutDate { get; set; }
    }
}
