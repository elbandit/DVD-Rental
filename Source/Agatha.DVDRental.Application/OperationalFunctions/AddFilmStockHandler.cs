using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Application.Commands;
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
