namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public class RequestFulfilled
    {
        public int FilmId { get; set; }
        public int SubscriptionId { get; set; }

        public RequestFulfilled(int filmId, int subscriptionId)
        {
            FilmId = filmId;
            SubscriptionId = subscriptionId;
        }
    }
}