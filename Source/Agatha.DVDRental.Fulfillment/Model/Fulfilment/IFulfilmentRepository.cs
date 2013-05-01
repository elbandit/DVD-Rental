using System.Collections.Generic;

namespace Agatha.DVDRental.Fulfillment.Model.Fulfilment
{
    public interface IFulfilmentRepository
    {
        IEnumerable<FulfilmentRequest> FindBy(int filmId, int subscriptionId);
        void Add(FulfilmentRequest fulfilmentRequest);
    }
}
