using MongoDB.Driver;

namespace API1.Database
{
    public class MongoRepository
    {
        public MongoClient client { set; get; }
        public IMongoDatabase database { set; get; }

        public MongoRepository(String connectionString)
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("BlockChainDocsDB");
        }
    }
}
