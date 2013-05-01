using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Operational.ApplicationService;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class HomeController : Controller
    {
        private OperationService _operationService;

        public HomeController(OperationService operationService)
        {
            _operationService = operationService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult AddFilm()
        {
            _operationService.AddFilmToCatalogue("dfff");

            return RedirectToAction("Index");
        }
    }
}
