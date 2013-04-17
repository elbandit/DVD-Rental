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
            get {

                if (_DocumentStore == null)
                {
                    _DocumentStore = new DocumentStore {Url = "http://localhost:8081"};
                    _DocumentStore.Initialize();
                    //_DocumentStore.Conventions.JsonContractResolver = new IncludeNonPublicMembersContractResolver();
                    _DocumentStore.DatabaseCommands.EnsureDatabaseExists("AgathasDVDRentals");
                }

                return _DocumentStore;
            }
        }
    }

    public class IncludeNonPublicMembersContractResolver : DefaultContractResolver
    {
        public IncludeNonPublicMembersContractResolver()
            {
                    DefaultMembersSearchFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            }

            protected override System.Collections.Generic.List<MemberInfo> GetSerializableMembers(System.Type objectType)
            {
            return base.GetSerializableMembers(objectType).Where(m => m.MemberType == MemberTypes.Property).ToList();
            }
    }
}
