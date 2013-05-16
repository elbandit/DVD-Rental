using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Public.ApplicationService;
using System.Web.Security;
using Agatha.DVDRental.Subscription.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Subscription.Model.Subscriptions;

namespace Agatha.DVDRental.Ui.Controllers
{
    public class RentalListController : Controller
    {
        private readonly RentingService _renting;
        private readonly Application _application;
        private ISubscriptionRepository _subscriptionRepository;

        public RentalListController(RentingService renting, Application application, ISubscriptionRepository subscriptionRepository)
        {
            _renting = renting;
            _application = application;
            _subscriptionRepository = subscriptionRepository;
        }

        //
        // GET: /RentalRequestList/

        public ActionResult Index()
        {
            var rentalList = _renting.ViewRentalListFor(User.Identity.Name);

            return View(rentalList);
        }

        [HttpPost]
        public ActionResult AddFilmToList(int filmId)
        {                                   
             var subscription = _subscriptionRepository.FindBy(User.Identity.Name);

             _application.action_request_to(new CustomerWantsToRentAFim() { FilmId = filmId, SubscriptionId = subscription.Id });

            return RedirectToAction("Index", "Films");
        }

        [HttpPost]
        public ActionResult RemoveFilmFromList(int filmId)
        {            
            var subscription = _subscriptionRepository.FindBy(User.Identity.Name);

            _application.action_request_to(new CustomerIsNotInterestedInRentingThisFim() { FilmId = filmId, SubscriptionId = subscription.Id });

            return RedirectToAction("Index", "Films");
        }

    
    }
}
