using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Agatha.DVDRental.Ui
{
    public class BootStrapper
    {
        public static void ConfigureDependencies()
        {
            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry<ControllerRegistry>();

                                         });
        }

        public class ControllerRegistry : Registry
        {
            public ControllerRegistry()
            {
               

                For<IDocumentSession>().LifecycleIs(new HttpSessionLifecycle()).Use(DocumentStoreFactory.DocumentStore.OpenSession);
            }
        }
    }
}