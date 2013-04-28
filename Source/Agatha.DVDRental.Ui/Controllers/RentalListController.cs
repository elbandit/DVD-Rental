using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Ui.Application;

namespace Agatha.DVDRental.Ui.Controllers
{
    public class RentalListController : Controller
    {
        private readonly Renting _renting;

        public RentalListController(Renting renting)
        {
            _renting = renting;            
        }

        //
        // GET: /RentalRequestList/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFilmToList(int filmId)
        {
            _renting.CustomerWantsToRentTheFim(filmId, 1);

            return RedirectToAction("Index", "Films");
        }

        [HttpPost]
        public ActionResult RemoveFilmFromList(int filmId)
        {
            _renting.CustomerDoesNotWantToRentTheFim(filmId, 1);

            return RedirectToAction("Index", "Films");
        }
    }
}
