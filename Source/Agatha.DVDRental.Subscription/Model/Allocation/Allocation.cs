using System;

namespace Agatha.DVDRental.Subscription.Model.Allocation
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
