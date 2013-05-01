﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Public.ApplicationService;

namespace Agatha.DVDRental.Ui.Controllers
{
    public class FilmsController : Controller
    {
        private readonly RentingService _renting;

        public FilmsController(RentingService renting)
        {
            _renting = renting;            
        }

        //
        // GET: /Films/

        public ActionResult Index()
        {
            var films = _renting.CustomerWantsToViewFilmsAvailableForRent(User.Identity.Name);

            return View(films);
        }
        
    }
}
