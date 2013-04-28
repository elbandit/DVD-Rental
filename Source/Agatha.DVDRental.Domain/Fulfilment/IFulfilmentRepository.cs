using System.Collections.Generic;

namespace Agatha.DVDRental.Domain.Fulfilment
{
    public interface IFulfilmentRepository
    {
        IEnumerable<FulfilmentRequest> FindBy(int subscriptionId);
    }
}
