using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Infrastructure;

namespace Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases
{
    public class AddStock : IBusinessUseCase
    {
        public int FilmId { get; set; }

        public string Barcode { get; set; }
    }
}
