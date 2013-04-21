using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Application.CustomerFunctions;
using Agatha.DVDRental.ApplicationService;

namespace Agatha.DVDRental.Ui.Controllers
{
    public class FilmsController : Controller
    {
        private readonly Renting _renting;

        public FilmsController( Renting renting)
        {
            _renting = renting;            
        }

        //
        // GET: /Films/

        public ActionResult Index()
        {
            var films = _renting.CustomerWantsToViewFilmsAvailableForRent(1);

            return View(films);
        }
        
    }
}
