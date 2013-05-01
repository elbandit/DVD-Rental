using System.Collections.Generic;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using Raven.Client;
using System.Linq;

namespace Agatha.DVDRental.Fulfillment.Infrastructure
{
    public class DvdRepository : IDvdRepository
    {
        private readonly IDocumentSession _documentSession;

        public DvdRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Dvd FindBy(int dvdId)
        {
            return _documentSession.Load<Dvd>(dvdId);
        }

        public IEnumerable<Dvd> FindAllBy(int filmId)
        {
            return _documentSession.Query<Dvd>().Where(x => x.FilmId == filmId);
        }

        public void Add(Dvd dvd)
        {
            _documentSession.Store(dvd);
        }
    }
}
