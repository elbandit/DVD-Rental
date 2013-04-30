using System;
using System.Collections.Generic;
using Agatha.DVDRental.Subscription.Model.RentalHistory;

namespace Agatha.DVDRental.Subscription.Model.Subscriptions
{
    public class Subscription
    {        
        public Subscription(Package package)
        {
            Package = package;
        }

        public int Id { get; set; }

        public Package Package { get; set; }
             
        public int PaymentHolidays { get; set; } // Payment Holiday class

        // public bool CanRentAfilm(CurrentPeriodRentals rentals)

        public void StartPaymentHoliday(DateTime endDate, CurrentPeriodRentals rentals)
        {
            // DomainEvents.Raise(new CannotStartPaymentHoliday())

            // DomainEvents.Raise(new PaymentHolidayStarted()) // Will pause billing
        }

        public void Cancel(CurrentPeriodRentals rentals)
        {
            //if (Package.IsSatisfiedBy(loans))
                // Yes
                //  DomainEvents.Raise(new PackageCancelled())
            // else
                //  No
                //  DomainEvents.Raise(new PackageCannotBeCancelled())
        }

        public void ChangePackage(Package package, CurrentPeriodRentals rentals)
        {
            if (package.IsADowngradeFrom(this.Package))
            {
                // check to see if we are allowed to do this?
                // If
                //  DomainEvents.Raise(new CannotDownGradePackage())
                // else
                //   DomainEvents.Raise(new PackageChanged())
            }
            else
            {
                this.Package = package;
                // DomainEvents.Raise(new SubscriptionChanged())
            }
        }

        public bool IsEligibleToRecieveAFilm(CurrentPeriodRentals currentPeriodRentals, IEnumerable<Allocation.Allocation> currentFulfilmentRequests)
        {
            throw new NotImplementedException();
        }
    }
}
