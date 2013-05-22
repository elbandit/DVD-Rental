using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Public.ApplicationService;
using Agatha.DVDRental.Subscription.Model.RentalHistory;

namespace Agatha.DVDRental.Ui.Controllers
{
    public class RentalHistoryController : Controller
    {
         private readonly RentingService _renting;

         public RentalHistoryController(RentingService renting)
        {
            _renting = renting;            
        }

        //
        // GET: /RentalHistory/

        public ActionResult Index()
        {
            IEnumerable<Rental> rentalHistory =_renting.GetRentalHistoryFor(User.Identity.Name);

            return View(rentalHistory);
        }

    }
}
