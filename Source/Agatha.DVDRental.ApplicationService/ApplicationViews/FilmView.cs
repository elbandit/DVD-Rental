namespace Agatha.DVDRental.ApplicationService.ApplicationViews
{
    public class FilmView
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsCurrentlyOnLoan { get; set; }
        public bool IsOnRentalList { get; set; }
    }
}