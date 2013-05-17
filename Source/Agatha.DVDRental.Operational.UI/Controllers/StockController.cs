using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Operational.ApplicationService;
using Agatha.DVDRental.Operational.UI.Models;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class StockController : Controller
    {
        private OperationService _operationService;
        private readonly Application _application;

        public StockController(OperationService operationService, Application application)
        {
            _operationService = operationService;
            _application = application;
        }

        public ActionResult ViewStockFor(int filmId)
        {
            var stockModel = new StockModel();
           IEnumerable<Dvd> stock = _operationService.ViewStockFor(filmId);

            stockModel.Stock = stock;
            stockModel.FilmId = filmId;

           return View(stockModel);
        }
        
        [HttpPost]
        public ActionResult AddStockFor(int filmId)
        {
            // Generate Barcode
            var barcode = Guid.NewGuid().ToString(); // This would be a call to an infrastructure service
          
            _application.action_request_to(new AddStock(){FilmId = filmId,Barcode = barcode});

            return RedirectToAction("StockAdded", new { filmId = filmId, barcode = barcode });
        }

        public ActionResult StockAdded(int filmId, string barcode)
        {
            var stockAddedModel = new StockAddedModel();

            stockAddedModel.FilmId = filmId;
            stockAddedModel.Barcode = barcode;

            return View(stockAddedModel);
        }
    }
}
