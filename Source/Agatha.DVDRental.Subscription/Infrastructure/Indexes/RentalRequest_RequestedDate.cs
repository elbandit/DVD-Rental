using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Agatha.DVDRental.Subscription.Infrastructure.Indexes
{
    public class RentalRequest_RequestedDate : AbstractIndexCreationTask<RentalRequestList, RentalRequest_RequestedDate.ReduceResult>
    {
        public class ReduceResult
		{
            public DateTimeOffset Requested { get; set; }
			public int SubscriptionId { get; set; }
			public int FilmId { get; set; }
		}

        public RentalRequest_RequestedDate()
        {
            Map = rentalRequestLists => from rentalRequestList in rentalRequestLists
                                        from rentalRequest in rentalRequestList.RentalRequests
                                        where rentalRequest.Status == ""                       
			                            select new
			                                 {
                                                rentalRequest.Requested,
                                                rentalRequest.FilmId,
                                                SubscriptionId = rentalRequestList.Id,			                             	    			                             	    
			                                 };

            Store(x => x.Requested, FieldStorage.Yes);
            Store(x => x.SubscriptionId, FieldStorage.Yes);
            Store(x => x.FilmId, FieldStorage.Yes);
			
		}
    }
}
