using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Operational.ApplicationService;
using Agatha.DVDRental.Operational.UI.Models;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class AddFilmController : Controller
    {       
        private readonly Application _application;

        public AddFilmController(Application application)
        {    
            _application = application;
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
            _application.action_request_to(new AddFilmToCatalogue(){ReleaseDate = DateTime.Now, Title = filmModel.Title});

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
