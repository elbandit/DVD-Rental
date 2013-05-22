using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Subscription.Model.Allocation;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy.FulfillmentIntegration
{
    public class StockAddedHandler : IHandleMessages<ACopyOfAFilmHasBeenAddedToTheStock>
    {
        private IAllocationRepository _allocationRepository;       

        public StockAddedHandler(IAllocationRepository allocationRepository)
        {
            _allocationRepository = allocationRepository;
        }

        public void Handle(ACopyOfAFilmHasBeenAddedToTheStock message)
        {
            var filmAllocations = _allocationRepository.FindBy(message.FilmId);

            if (filmAllocations == null)
            {
                filmAllocations = new Allocation(message.FilmId, 1);
                _allocationRepository.Add(filmAllocations);
            }
            else
            {
                filmAllocations.IncreaseStock();
            }
        }
    }
}
