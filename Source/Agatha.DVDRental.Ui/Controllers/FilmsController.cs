using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agatha.DVDRental.Ui.Controllers
{
    public class FilmsController : Controller
    {
        //
        // GET: /Films/

        public ActionResult Index()
        {
            return View();
        }

    }
}
