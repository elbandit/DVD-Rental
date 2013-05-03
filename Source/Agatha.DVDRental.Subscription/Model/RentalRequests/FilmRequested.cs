namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public class FilmRequested
    {
        public FilmRequested(int filmId, int subscriptionId)
        {
            FilmId = filmId;
            SubscriptionId = subscriptionId;
        }

        public int FilmId { get; set; }

        public int SubscriptionId { get; set; }
    }
}
