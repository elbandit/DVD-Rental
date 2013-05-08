namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public interface IRentalRequestRepository
    {
        RentalRequestList FindBy(int memberId);
        RentalRequest FindBy(int subscriptionId, int filmId);
        void Add(RentalRequest request);
    }
}
