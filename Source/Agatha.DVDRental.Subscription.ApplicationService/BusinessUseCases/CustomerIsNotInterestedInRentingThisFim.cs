using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Infrastructure;

namespace Agatha.DVDRental.Subscription.ApplicationService.BusinessUseCases
{
    public class CustomerIsNotInterestedInRentingThisFim : IBusinessUseCase
    {
        public int FilmId { get; set; }

        public int SubscriptionId { get; set; }
    }
}
