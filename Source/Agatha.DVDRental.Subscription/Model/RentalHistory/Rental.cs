using System;

namespace Agatha.DVDRental.Subscription.Model.RentalHistory
{
    public class Rental
    {       
        public Rental(int dvdId, int subscriptionId, DateTime sentOut)
        {
            DvdId = dvdId;
            SubscriptionId = subscriptionId;
            DateSentOut = sentOut;            
        }

        public int DvdId { get; private set; }
        public int SubscriptionId { get; private set; }
        public DateTime DateSentOut { get; private set; }
        public DateTime DateReturned { get; private set; }
    }
}
