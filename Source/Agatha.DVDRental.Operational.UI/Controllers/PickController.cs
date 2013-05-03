using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Operational.ApplicationService;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class PickController : Controller
    {
        private OperationService _operationService;

        public PickController(OperationService operationService)
        {
            _operationService = operationService;
        }

        //
        // GET: /Pick/

        public ActionResult Index()
        {
            IEnumerable<FulfilmentRequest> all = _operationService.ViewAllFulfilmentRequests();

            return View(all);
        }

        [HttpPost]
        public ActionResult Assign()
        {
            _operationService.OperatorWantsToPickRentalAllocations("Scott");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Dispatch()
        {
            _operationService.OperatorWantsToMarkRentalAllocationsAsDispatched("Scott");

            return RedirectToAction("Index");
        }

    }
}
