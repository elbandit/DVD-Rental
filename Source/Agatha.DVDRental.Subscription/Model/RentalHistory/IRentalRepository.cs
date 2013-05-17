namespace Agatha.DVDRental.Subscription.Model.RentalHistory
{
    public interface IRentalRepository
    {
        CurrentPeriodRentals FindRentalsForCurrentPeriod();
        void Add(Rental rental);
    }
}
