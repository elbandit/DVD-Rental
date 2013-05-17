namespace Agatha.DVDRental.Subscription.Contracts.Events
{
    public class FilmHasBeenAllocated
    {
        public int FilmId { get; set; }

        public int SubscriptionId { get; set; }
    }
}
