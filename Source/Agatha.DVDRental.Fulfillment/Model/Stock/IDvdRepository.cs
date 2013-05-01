using System.Collections.Generic;

namespace Agatha.DVDRental.Fulfillment.Model.Stock
{
    public interface IDvdRepository
    {
        Dvd FindBy(int dvdId);
        IEnumerable<Dvd> FindAllBy(int filmId);
        void Add(Dvd dvd);
    }
}
