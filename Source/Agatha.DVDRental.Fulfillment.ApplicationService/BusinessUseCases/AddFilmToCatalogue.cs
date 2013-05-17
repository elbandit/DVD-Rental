using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Infrastructure;

namespace Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases
{
    public class AddFilmToCatalogue : IBusinessUseCase
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
