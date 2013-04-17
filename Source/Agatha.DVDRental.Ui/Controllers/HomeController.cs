using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Application.Commands;
using Agatha.DVDRental.Messages.CustomerScenarios.Commands;
using NServiceBus;

namespace Agatha.DVDRental.Ui.Controllers
{
    public class HomeController : Controller
    {       
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {

            // Don't send on bus call application service and it can send on bus if it needs to... Application service can validate.
            MvcApplication.Bus.Send(new CustomerWantsToRentFilm() { FilmId = 1, MemberId = 2 });

            return View();
        }
    }
}
