using API1.Database;
using API1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API1.Interfaces

{
    class UserCollection : IUser
    {
        private MongoRepository mongo;
        private IMongoCollection<User> collection;

        public UserCollection(IConfiguration configuration)
        {
            mongo = new MongoRepository(configuration.GetConnectionString("BlockChainAppCon"));
            collection = mongo.database.GetCollection<User>("User");
        }

        public string deleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq("userId", id);
            if (collection.DeleteOne(filter).ToString().Contains("Acknowledged"))
            {
                return "Se ha eliminado correctamente";
            }
            return "No se ha eliminado!!";
        }

        public List<User> getUsers()
        {
            return collection.AsQueryable().ToList();
        }

        public List<FileM> postUser(User user)
        {
            throw new NotImplementedException();
        }

        public User validateUser(User user)
        {
            var dbList = mongo.database.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("userName", user.userName);
            var singleUser = dbList.Find(filter).FirstOrDefault();

            if (singleUser is null)
            {
                return null;
            }
            else
            {
                if (singleUser.password == user.password)
                {
                    return singleUser;
                    //return new JsonResult(Response.StatusCode);
                }
                else
                {
                    return null;
                }

            }
        }

        List<User> IUser.postUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}