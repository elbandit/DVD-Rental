namespace Agatha.DVDRental.Subscription.Model.RentalRequests
{
    public class RentalRequestRemoved
    {
        public int FilmId { get; set; }
        public int MemberId { get; set; }

        public RentalRequestRemoved(int filmId, int memberId)
        {
            FilmId = filmId;
            MemberId = memberId;
        }
    }
}