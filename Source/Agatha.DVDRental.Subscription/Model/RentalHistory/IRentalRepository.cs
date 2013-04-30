namespace Agatha.DVDRental.Subscription.Model.RentalHistory
{
    public interface IRentalRepository
    {
        CurrentPeriodRentals FindRentalsForCurrentPeriod();
    }
}
