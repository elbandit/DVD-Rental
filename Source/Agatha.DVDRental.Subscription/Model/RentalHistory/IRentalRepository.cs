namespace Agatha.DVDRental.Subscription.Model.RentalHistory
{
    public interface IRentalRepository
    {
        CurrentPeriodRentals FindRentalsForCurrentBillingPeriod();
        void Add(Rental rental);
    }
}
