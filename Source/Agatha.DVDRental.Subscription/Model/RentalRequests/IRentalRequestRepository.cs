using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Subscription.Model.RentalRequests;

namespace Agatha.DVDRental.Domain.RentalLists
{
    public interface IRentalRequestRepository
    {
        RentalRequestList FindBy(int memberId);
        void Add(RentalRequest request);
    }
}
