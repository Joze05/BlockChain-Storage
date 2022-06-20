using API1.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        //Get all users from mongo DB
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            var dbList = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<User>("User").AsQueryable();

            return new JsonResult(dbList);
        }

        [HttpPost("validate")]
        public JsonResult ValidateUser(User user)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));
            var dbList = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("userName", user.userName);
            var singleUser = dbList.Find(filter).FirstOrDefault();
            if (singleUser is null)
            {
                return new JsonResult(null);
            }
            else
            {
                if (singleUser.password == user.password)
                {
                    return new JsonResult(singleUser);
                }
                else
                {
                    return new JsonResult(null);
                }
            }
        }

        [HttpPost]
        //Add user to mongo DB
        public JsonResult Post(User user)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            int LastUserId = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<User>("User").AsQueryable().Count();
            user.userId = LastUserId + 1;

            dbClient.GetDatabase("BlockChainDocsDB").GetCollection<User>("User").InsertOne(user);

            return new JsonResult("Registrado Correctamente");
        }


        [HttpPut]
        //Update/modify user from mongo DB
        public JsonResult Put(User user)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            var filter = Builders<User>.Filter.Eq("userId", user.userId);

            var update = Builders<User>.Update.Set("userName", user.userName);

            dbClient.GetDatabase("BlockChainDocsDB").GetCollection<User>("User").UpdateOne(filter, update);

            return new JsonResult("Actualizado Correctamente");
        }

        [HttpDelete("{id}")]
        //Add user to mongo DB
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            var filter = Builders<User>.Filter.Eq("userId", id);

            dbClient.GetDatabase("BlockChainDocsDB").GetCollection<User>("User").DeleteOne(filter);

            return new JsonResult("Eliminado Correctamente");
        }
    }
}
