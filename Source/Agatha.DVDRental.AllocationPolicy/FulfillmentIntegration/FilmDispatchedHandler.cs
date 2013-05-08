using Agatha.DVDRental.Fulfillment.Contracts;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy.FulfillmentIntegration
{
    public class FilmDispatchedHandler : IHandleMessages<FilmDispatched>
    {
        public void Handle(FilmDispatched message)
        {
            throw new System.NotImplementedException();
        }
    }
}
