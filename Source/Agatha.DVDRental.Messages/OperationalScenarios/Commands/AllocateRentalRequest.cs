using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace Agatha.DVDRental.Messages.OperationalScenarios.Commands
{
    public class AllocateRentalRequest : ICommand
    {
        public int SubscriptionId { get; set; }

        public int FilmId { get; set; }
    }
}
