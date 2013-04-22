using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.Films
{
    public interface IFilmRepository
    {
        Film FindBy(int filmId);
    }
}
