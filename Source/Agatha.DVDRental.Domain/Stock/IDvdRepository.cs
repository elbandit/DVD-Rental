using System.Collections.Generic;

namespace Agatha.DVDRental.Domain.Stock
{
    public interface IDvdRepository
    {
        Stock.Dvd FindBy(int dvdId);
        IEnumerable<Stock.Dvd> FindAllBy(int filmId);
    }
}
