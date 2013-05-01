using System;

namespace Agatha.DVDRental.Catalogue.Catalogue
{
    public class Film
    {
        public Film(DateTime releaseDate)
        {          
            ReleaseDate = releaseDate;           
        }
       
        public int Id { get; private set; }        
        public DateTime ReleaseDate { get; private set; }
        public string Title { get; set; }
        public string Certification { get; set; }
    }
}
