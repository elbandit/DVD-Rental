using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Messages.OperationalScenarios.Commands;
using NServiceBus;

namespace Agatha.DVDRental.Application.OperationalFunctions
{
    public class AddFilmStockHandler : IHandleMessages<AddFilmToStock>
    {
        public void Handle(AddFilmToStock message)
        {
            
        }
    }
}
