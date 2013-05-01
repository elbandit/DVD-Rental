using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Public.ApplicationService;
using System.Web.Security;

namespace Agatha.DVDRental.Ui.Controllers
{
    public class RentalListController : Controller
    {
        private readonly RentingService _renting;

        public RentalListController(RentingService renting)
        {
            _renting = renting;            
        }

        //
        // GET: /RentalRequestList/

        public ActionResult Index()
        {
            var rentalList = _renting.ViewRentalListFor(User.Identity.Name);

            return View(rentalList);
        }

        [HttpPost]
        public ActionResult AddFilmToList(int filmId)
        {                        
            _renting.CustomerWantsToRentTheFim(filmId, User.Identity.Name);

            return RedirectToAction("Index", "Films");
        }

        [HttpPost]
        public ActionResult RemoveFilmFromList(int filmId)
        {
            _renting.CustomerDoesNotWantToRentTheFim(filmId, User.Identity.Name);

            return RedirectToAction("Index", "Films");
        }

    
    }
}
