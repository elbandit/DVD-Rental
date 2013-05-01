using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Operational.ApplicationService;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class FilmController : Controller
    {
        private OperationService _operationService;

        public FilmController(OperationService operationService)
        {
            _operationService = operationService;
        }

        //
        // GET: /Film/

        public ActionResult Index()
        {
            IEnumerable<Film> films = _operationService.ViewAllFilms();

            return View(films);
        }

    }
}
