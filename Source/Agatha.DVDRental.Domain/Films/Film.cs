using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.Films
{
    public class Film
    {
        public Film(DateTime releaseDate)
        {          
            ReleaseDate = releaseDate;           
        }
       
        public int Id { get; private set; }        
        public DateTime ReleaseDate { get; private set; }     
    }
}
