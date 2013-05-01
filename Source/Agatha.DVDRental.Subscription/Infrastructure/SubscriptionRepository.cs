using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Subscription.Model.Subscriptions;
using Raven.Client;

namespace Agatha.DVDRental.Subscription.Infrastructure
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IDocumentSession _documentSession;

        public SubscriptionRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Model.Subscriptions.Subscription FindBy(int subscriptionId)
        {
            return _documentSession.Load<Model.Subscriptions.Subscription>(subscriptionId);
        }

        public Model.Subscriptions.Subscription FindBy(string subscriptionEmail)
        {
            return _documentSession.Query<Model.Subscriptions.Subscription>()
                .SingleOrDefault(x => x.EmailAddress == subscriptionEmail);
        }

        public void Add(Model.Subscriptions.Subscription subscription)
        {
            _documentSession.Store(subscription);
            _documentSession.SaveChanges();
        }
    }
}
