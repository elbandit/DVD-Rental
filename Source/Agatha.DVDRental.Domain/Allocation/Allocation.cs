using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.Allocation
{
    public class Allocation
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int Stock { get; set; }
        public int Available { get; set; }

        public bool StockAvailble()
        {
            throw new NotImplementedException();
        }

        public void AllocateUnitTo(int subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}
