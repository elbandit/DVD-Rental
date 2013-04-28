namespace Agatha.DVDRental.Domain.Subscriptions
{
    public interface ISubscriptionRepository
    {
        Subscription FindBy(int subscriptionId);
    }
}
