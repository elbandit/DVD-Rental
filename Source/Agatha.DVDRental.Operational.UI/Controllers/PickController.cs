using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Operational.ApplicationService;
using Agatha.DVDRental.Operational.ApplicationService.ApplicationViews;
using Agatha.DVDRental.Operational.UI.Models;

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
            PickListView pickListView = _operationService.OperatorWantsToViewAssignedRentalAllocations("Scott");

            return View(pickListView);
        }

        [HttpPost]
        public ActionResult Assign()
        {
            _operationService.OperatorWantsToPickRentalAllocations("Scott");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Dispatch(DvdAssignmentModel form)
        {
            _operationService.OperatorWantsToMarkRentalAllocationsAsDispatched("Scott", form.FulfilmentRequestId, form.DvdId);

            return RedirectToAction("Index");
        }

    }
}
