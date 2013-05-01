using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using Agatha.DVDRental.Operational.ApplicationService;
using Agatha.DVDRental.Operational.UI.Models;

namespace Agatha.DVDRental.Operational.UI.Controllers
{
    public class StockController : Controller
    {
        private OperationService _operationService;

        public StockController(OperationService operationService)
        {
            _operationService = operationService;
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
            _operationService.OperatorWantsToAddStock(filmId, "xxxx");

            return RedirectToAction("ViewStockFor", new {filmId = filmId});
        }

    }
}
