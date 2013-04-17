using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.RentalLists
{
    public interface IRentalListRepository
    {
        RentalList FindBy(int memberId);
        void Add(RentalRequest request);
    }
}
