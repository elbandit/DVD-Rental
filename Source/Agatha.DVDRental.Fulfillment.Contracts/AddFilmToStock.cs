namespace Agatha.DVDRental.Fulfillment.Contracts
{
    public class AddFilmToStock
    {
        public int FilmId { get; set; }
        public string Barcode { get; set; }
    }
}
