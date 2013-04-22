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
            return View();
        }
    }
}
