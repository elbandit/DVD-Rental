namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public class FilmRequested
    {
        public FilmRequested(int filmId, int subscriptionId, string id)
        {
            FilmId = filmId;
            SubscriptionId = subscriptionId;
            Id = id;
        }

        public int FilmId { get; set; }

        public int SubscriptionId { get; set; }

        public string Id { get; set; }
    }
}
