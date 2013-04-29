namespace Agatha.DVDRental.Domain.RentalHistory
{
    public interface IRentalRepository
    {
        CurrentPeriodRentals FindRentalsForCurrentPeriod();
    }
}
