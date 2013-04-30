using System;

namespace Agatha.DVDRental.Fulfillment.Model.Fulfilment
{
    public class FulfilmentRequest
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int StockCount { get; set; }

        public DateTime Allocated { get; private set; }
        public string AssignedTo { get; set; } // the person who is going to pick the Dvd. Fires 'Ready to dispatch' event        
    }
}
