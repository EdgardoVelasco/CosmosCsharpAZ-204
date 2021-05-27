using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos;

namespace CosmosDBM4.cosmos
{
    class DatabaseClient
    {
        private static readonly string COSMOS_URL = "https://cosmosenvf.documents.azure.com:443/";
        private static readonly string COSMOS_KEY = "OWJbJak9Ic43BzhXmURclUo3p0u9JQsHEZL8YZ2HdyExctsHjbO6z7HPeN17e7dXRaXBJ1V9RJ1rTj3nexRTuA==";
        private static readonly string DATABASE_NAME = "Tienda";
        public static Database Conex { get; private set;}

        static DatabaseClient() { InitDatabase(); }

        private static void InitDatabase() {
            CosmosClient cliente = new CosmosClient(COSMOS_URL, COSMOS_KEY);
            Conex = Conex != null ? Conex : cliente.GetDatabase(DATABASE_NAME);
        }
    }
}
