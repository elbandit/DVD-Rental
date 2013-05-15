using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy
{
    public class AssignDvdToSubscriptionHandler : IHandleMessages<AssignDvdToSubscription>
    {
        private IDvdRepository _dvdRepository;

        public AssignDvdToSubscriptionHandler(IDvdRepository dvdRepository)
        {
            _dvdRepository = dvdRepository;
        }

        public void Handle(AssignDvdToSubscription message)
        {
            var dvd = _dvdRepository.FindBy(message.DvdId);

            dvd.LoanTo(message.SubscriptionId);
        }
    }
}
