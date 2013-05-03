using Raven.Client.Document;

namespace Agatha.DVDRental.Fulfillment.Infrastructure
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
