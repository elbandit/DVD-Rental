namespace Agatha.DVDRental.Subscription.Model.Subscriptions
{
    public interface ISubscriptionRepository
    {
        Subscription FindBy(int subscriptionId);
        Subscription FindBy(string subscriptionId);
        void Add(Subscription subscription);
    }
}
