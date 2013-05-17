using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Subscription.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Subscription.Model.Subscriptions;

namespace Agatha.DVDRental.Subscription.ApplicationService.Handlers
{
    public class RegisterHandler : IBusinessUseCaseHandler<Register>
    {
        private ISubscriptionRepository _subscriptionRepository;

        public RegisterHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public void action(Register businessUseCase)
        {
            var package = new Package();
            package.DiscsOutAtSameTime = 1;
            package.StartDate = DateTime.Now;

            var subscription = new Model.Subscriptions.Subscription(package);

            subscription.EmailAddress = businessUseCase.EmailAddress;

            _subscriptionRepository.Add(subscription);
        }
    }
}
