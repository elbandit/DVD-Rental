using NServiceBus;

namespace Agatha.DVDRental.Messages.OperationalScenarios.Events
{
    public class ReturnAFilm : ICommand
    {
        public int DvdId { get; set; }
    }
}
