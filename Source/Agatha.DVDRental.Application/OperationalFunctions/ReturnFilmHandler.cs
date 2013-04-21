using Agatha.DVDRental.Application.Commands;
using Agatha.DVDRental.Domain.Films;
using NServiceBus;


namespace Agatha.DVDRental.Application.OperationalFunctions
{
    public class ReturnFilmHandler : IHandleMessages<ReturnAFilm>
    {
        public void Handle(ReturnAFilm message)
        {
            
        }
    }
}
