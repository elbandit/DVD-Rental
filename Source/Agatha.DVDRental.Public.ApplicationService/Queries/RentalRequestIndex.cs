using System.Linq;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Public.ApplicationService.ApplicationViews;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Agatha.DVDRental.Public.ApplicationService.Queries
{
    public class RentalRequestIndex : AbstractIndexCreationTask<RentalRequestList, RentalRequestView>
    {
        public RentalRequestIndex()
        {


            Map = rentalRequestLists => from rentalRequestList in rentalRequestLists
                                        from rentalRequest in rentalRequestList.RentalRequests                                             
                                        select new RentalRequestView
                                                    {                                                        
                                                        SubscriptionId = int.Parse(rentalRequestList.Id.ToString().Split(new[] { '/' })[1]),
                                                        FilmId = rentalRequest.FilmId,
                                                        Requested = rentalRequest.Requested,
                                                        CanBeRemovedFromList = !rentalRequest.IsBeingPicked
                                                    };
                     
            Store(x => x.SubscriptionId, FieldStorage.Yes);
            Store(x => x.FilmId, FieldStorage.Yes);
            Store(x => x.Requested, FieldStorage.Yes);
            Store(x => x.CanBeRemovedFromList, FieldStorage.Yes);

            TransformResults = (database, results) => from result in results
                                                      let problem =
                                                          database.Load<Film>("films/" + result.FilmId.ToString())
                                                      select new RentalRequestView
                                                                 {                                                                    
                                                                     FilmTitle = problem.Title,
                                                                     SubscriptionId = result.SubscriptionId,
                                                                     FilmId = result.FilmId,
                                                                     Requested = result.Requested,
                                                                     CanBeRemovedFromList = result.CanBeRemovedFromList
                                                                 };
        }
    }  
}
