using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Catalogue.Infrastructure.Indexes;
using Agatha.DVDRental.Public.ApplicationService.ApplicationViews;
using Agatha.DVDRental.Public.ApplicationService.Queries;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using Agatha.DVDRental.Ui.Controllers;
using AutoMapper;
using NServiceBus;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using StructureMap;

namespace Agatha.DVDRental.Ui
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        //public MvcApplication()
        //{           
        //    EndRequest += (sender, args) =>
        //    {
        //        using (var session = (IDocumentSession)ObjectFactory.TryGetInstance<IDocumentSession>())
        //        {
        //            if (session == null)
        //                return;

        //            if (Server.GetLastError() != null)
        //                return;

        //            session.SaveChanges();
        //        }                
        //    };
        //}

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);            

            DocumentStoreFactory.InitializeDocumentStore();

            IndexCreation.CreateIndexes(typeof(Films).Assembly, DocumentStoreFactory.DocumentStore);
            IndexCreation.CreateIndexes(typeof(RentalRequestIndex).Assembly, DocumentStoreFactory.DocumentStore);

            BootStrapper.ConfigureDependencies();

            Mapper.CreateMap<Film, FilmView>();
            Mapper.CreateMap<RentalRequest, RentalRequestView>();

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}