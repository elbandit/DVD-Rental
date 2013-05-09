using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agatha.DVDRental.Operational.UI.Models
{
    public class StockAddedModel
    {
        public int FilmId { get; set; }
        public string Barcode { get; set; }
    }
}