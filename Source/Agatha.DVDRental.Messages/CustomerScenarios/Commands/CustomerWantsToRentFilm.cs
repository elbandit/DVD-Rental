using NServiceBus;

namespace Agatha.DVDRental.Messages.CustomerScenarios.Commands
{
    public class CustomerWantsToRentFilm : ICommand
    {
        public int FilmId { get; set; }
        public int MemberId { get; set; }
    }
}
