using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Operational.ApplicationService;
using Agatha.DVDRental.Operational.ApplicationService.ApplicationViews;
using Agatha.DVDRental.Operational.UI.Models;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class PickController : Controller
    {
        private OperationService _operationService;
        private readonly Application _application;

        public PickController(OperationService operationService, Application application)
        {
            _operationService = operationService;
            _application = application;
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
            _application.action_request_to(new AssignRentalAllocationsToPicker() {PickerName = "Scott"});

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Dispatch(DvdAssignmentModel form)
        {            
            _application.action_request_to(new MarkRentalAllocationAsDispatched()
                                               {
                                                   PickerName = "Scott",
                                                   DvdId = form.DvdId,
                                                   FulfilmentRequestId = form.FulfilmentRequestId
                                               });

            return RedirectToAction("Index");
        }

    }
}
