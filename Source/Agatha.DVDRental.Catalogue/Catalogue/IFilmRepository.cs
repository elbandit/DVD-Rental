namespace Agatha.DVDRental.Catalogue.Catalogue
{
    public interface IFilmRepository
    {
        Film FindBy(int filmId);
        void Add(Film film);
    }
}
