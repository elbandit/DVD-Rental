using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Subscription.Model.Allocation;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy
{
    public class StockAddedHandler : IHandleMessages<FilmAddedToStock>
    {
        private IAllocationRepository _allocationRepository;

        public StockAddedHandler(IAllocationRepository allocationRepository)
        {
            _allocationRepository = allocationRepository;
        }

        public void Handle(FilmAddedToStock message)
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
