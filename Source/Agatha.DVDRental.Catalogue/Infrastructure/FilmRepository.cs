using Agatha.DVDRental.Domain.Films;
using Raven.Client;

namespace Agatha.DVDRental.Catalogue.Infrastructure
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
            return _documentSession.Load<Film>("Films/" + filmId);
        }
    }
}
