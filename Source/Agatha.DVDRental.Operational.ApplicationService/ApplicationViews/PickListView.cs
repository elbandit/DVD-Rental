using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Operational.ApplicationService.ApplicationViews
{
    public class PickListView
    {
        public IEnumerable<PickRequestView> PickRequests { get; set; }
    }

    public class PickRequestView
    {
        public string Id { get; set; }
        public int FilmId { get;  set; }
        public int SubscriptionId { get; set; }
        public DateTime Requested { get; set; }
        
        public string AssignedTo { get; set; }

        public IEnumerable<int> DvdIdsToFulfil { get; set; }
    }
}
