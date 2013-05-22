using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Agatha.DVDRental.Infrastructure
{
    public static class DocumentStoreFactory
    {
        private static IDocumentStore _DocumentStore;

        public static void InitializeDocumentStore()
        {
            if (_DocumentStore != null) return; // prevent misuse

            _DocumentStore = new DocumentStore
                                 {
                                     ConnectionStringName = "RavenDB"
                                 }.Initialize();

            _DocumentStore.DatabaseCommands.EnsureDatabaseExists("AgathasDVDRentals");
        }

        public static IDocumentStore DocumentStore
        {
            get
            {

                if (_DocumentStore == null)
                {
                    InitializeDocumentStore();
                }

                return _DocumentStore;
            }
        }
    }
}
