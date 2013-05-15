namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public interface IRentalRequestRepository
    {
        RentalRequestList FindBy(int subscriptionId);        
        void Add(RentalRequestList request);
    }
}
