using Agatha.DVDRental.Application.Commands;
using Agatha.DVDRental.Domain.Films;
using NServiceBus;


namespace Agatha.DVDRental.Application.OperationalFunctions
{
    public class ReturnFilmHandler : IHandleMessages<ReturnFilm>
    {
        public void Handle(ReturnFilm message)
        {
            
        }
    }
}
