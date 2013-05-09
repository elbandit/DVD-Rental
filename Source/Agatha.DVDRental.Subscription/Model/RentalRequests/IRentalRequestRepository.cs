namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public interface IRentalRequestRepository
    {
        RentalRequestList FindBy(int memberId);        
        void Add(RentalRequestList request);
    }
}
