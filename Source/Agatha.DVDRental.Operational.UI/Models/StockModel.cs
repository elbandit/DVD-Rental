using System.Collections.Generic;
using Agatha.DVDRental.Fulfillment.Model.Stock;

namespace Agatha.DVDRental.Operational.UI.Models
{
    public class StockModel
    {
        public int FilmId { get; set; }
        public IEnumerable<Dvd> Stock { get; set; }
    }
}