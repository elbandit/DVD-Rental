using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Subscription.Contracts.InternalCommands;
using Agatha.DVDRental.Subscription.Model.RentalHistory;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy
{
    public class AddRentalHistoryHandler : IHandleMessages<AddRentalHistory>
    {
        private IRentalRepository _rentalRepository;

        public AddRentalHistoryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public void Handle(AddRentalHistory message)
        {
            var rental = new Rental(message.FilmId, message.SubscriptionId, message.SentOutDate);

            _rentalRepository.Add(rental);
        }
    }
}
