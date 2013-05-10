using System;
using System.Linq;
using Agatha.DVDRental.Catalogue.Catalogue;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Agatha.DVDRental.Catalogue.Infrastructure.Indexes
{
    public class Films : AbstractIndexCreationTask<Film, FilmResult>
    {        
        public Films()
        {
            Map = films => from film in films
                           select new FilmResult
                                             {
                                                 Title = film.Title,
                                                 FilmId = int.Parse(film.Id.ToString().Split(new[] { '/' })[1])
                                             };
           
            Store(x => x.Title, FieldStorage.Yes);
            Store(x => x.FilmId, FieldStorage.Yes);
			
		}
    }

    public class FilmsUnTyped : AbstractIndexCreationTask
    {
        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition
            {
                Map = @"
                     from film in docs.Films
                           select new 
                            {
                                Title = film.Title,
                                FilmId = 5,
                                FilmId2 = 55,
                                Id = film.Id
                            };"
            };
        }
    }
}
