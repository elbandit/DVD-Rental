using System;
using Agatha.DVDRental.Subscription.Model.Subscriptions;

namespace Agatha.DVDRental.Public.ApplicationService
{
    public class SubscriptionService
    {
        private ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public void CustomerWantsToCancelSubscription(int subscriptionId)
        {

        }

        public void CustomerWantsToChangeSubscription(int subscriptionId)
        {

        }

        public void CustomerWantsToTakeAPaymentHoliday(int subscriptionId)
        {

        }

        public void CreateSubscription(string email)
        {
            var package = new Package();
            package.DiscsOutAtSameTime = 1;
            package.StartDate = DateTime.Now;

            var subscription = new Subscription.Model.Subscriptions.Subscription(package);

            subscription.EmailAddress = email;

            _subscriptionRepository.Add(subscription);
        }

        public bool AlreadyHaveSubscriptionWithEmail(string email)
        {
           var subscription =  _subscriptionRepository.FindBy(email);

            return subscription != null;
        }

        public Subscription.Model.Subscriptions.Subscription GetSubscriptionWithEmail(string email)
        {
            return _subscriptionRepository.FindBy(email);
        }
    }
}
