using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Operational.ApplicationService;
using Agatha.DVDRental.Operational.UI.Models;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class AddFilmController : Controller
    {
        private OperationService _operationService;

        public AddFilmController(OperationService operationService)
        {
            _operationService = operationService;
        }

        //
        // GET: /AddFilm/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FilmModel filmModel)
        {
            _operationService.AddFilmToCatalogue(filmModel.Title);

            return RedirectToAction("FilmAdded", new { Title = filmModel.Title });
        }
       
        public ActionResult FilmAdded(String title)
        {
            var filmAdded = new FilmAddedModel();
            filmAdded.Title = title;

            return View(filmAdded);
        }
    }
}
