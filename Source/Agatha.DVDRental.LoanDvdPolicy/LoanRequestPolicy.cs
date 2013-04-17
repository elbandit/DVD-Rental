using System;
using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Messages.CustomerScenarios;
using Agatha.DVDRental.Messages.CustomerScenarios.Events;
using NServiceBus;

namespace Agatha.DVDRental.LoanDvdPolicy
{
    public class LoanRequestPolicy :IHandleMessages<FilmRequestedMessage>
    {
        private IFilmRepository _filmRepository;

        public LoanRequestPolicy(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public void Handle(FilmRequestedMessage message)
        {
            // do we have stock to rent to DVD

            // Stock Service.. in stock
            // Monthly allowance
            // Fair usage policy

            Console.Out.WriteLine(@"FilmAddedToListHandler event fired: {0}", _filmRepository.SayHello());
        }

        
    }
}
