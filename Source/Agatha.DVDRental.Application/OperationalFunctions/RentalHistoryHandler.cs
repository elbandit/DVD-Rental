using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Messages.CustomerScenarios.Events;
using NServiceBus;

namespace Agatha.DVDRental.Application.OperationalFunctions
{
    public class RentalHistoryHandler : IHandleMessages<DvdReturnedMessage>
    {
        public void Handle(DvdReturnedMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
