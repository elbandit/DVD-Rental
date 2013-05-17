using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Infrastructure;

namespace Agatha.DVDRental.Subscription.ApplicationService.BusinessUseCases
{
    public class Register : IBusinessUseCase
    {
        public string EmailAddress { get; set; }
        public int PackageId { get; set; }
    }
}
