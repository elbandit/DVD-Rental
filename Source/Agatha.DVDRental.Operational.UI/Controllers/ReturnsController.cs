using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Operational.ApplicationService;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class ReturnsController : Controller
    {
        private OperationService _operationService;
        private readonly Application _application;

        public ReturnsController(OperationService operationService, Application application)
        {
            _operationService = operationService;
            _application = application;
        }

        //
        // GET: /Returns/

        public ActionResult Index()
        {
            var returns = _operationService.ViewAllPotentialReturns();

            return View(returns);
        }

        public ActionResult Process(int DvdId)
        {
            _application.action_request_to(new ReturnAFilm() {DvdId = DvdId});

            return RedirectToAction("ReturnProcessed");
        }

        public ActionResult ReturnProcessed()
        {
            return View();
        }
    }
}
