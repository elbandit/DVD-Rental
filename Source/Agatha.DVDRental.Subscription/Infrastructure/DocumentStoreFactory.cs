using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Imports.Newtonsoft.Json.Serialization;

namespace Agatha.DVDRental.Infrastructure
{
    public static class DocumentStoreFactory
    {
        private static DocumentStore _DocumentStore;

        public static DocumentStore DocumentStore
        {
            get
            {
                if (_DocumentStore == null)
                {
                    _DocumentStore = new DocumentStore { ConnectionStringName = "RavenDB" };
                    _DocumentStore.Initialize();                    
                }

                return _DocumentStore;
            }
        }
    }
}
