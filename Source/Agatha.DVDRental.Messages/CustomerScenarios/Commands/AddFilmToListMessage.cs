

namespace Agatha.DVDRental.Messages.CustomerScenarios
{
    public class AddFilmToListMessage 
    {
        public int FilmId { get; set; }
        public int MemberId { get; set; }
    }
}
