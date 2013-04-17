using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Domain.Films;
using Raven.Client;

namespace Agatha.DVDRental.Infrastructure
{
    public class FilmRepository : IFilmRepository
    {
        private readonly IDocumentSession _documentSession;

        public FilmRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Film FindBy(int filmId)
        {
            return _documentSession.Load<Film>(filmId);
        }

        public string SayHello()
        {
            return "hello";
        }
    }
}
