using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Infrastructure.Subscription.RentalHistory;

namespace Agatha.DVDRental.Domain.Rentals
{
    public interface IRentalRepository
    {
        CurrentPeriodRentals FindRentalsForCurrentPeriod();
    }
}
