using API1.Models;
using API1.Interfaces;
using API1.Database;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public MongoRepository mongo;
        private UserCollection collection;
        public UserController(IConfiguration configuration)
        {
            // _configuration = configuration;
            mongo = new MongoRepository(configuration.GetConnectionString("BlockChainAppCon"));
            collection = new UserCollection(configuration);
        }

        [HttpGet]
        //Get all users from mongo DB
        public JsonResult Get()
        {
            return new JsonResult(collection.getUsers());
        }

        [HttpPost("validate")]
        //Get single user from mongo DB
        public JsonResult Get(User user)
        {
            return new JsonResult(collection.validateUser(user));
        }

        [HttpPost]
        //Add user to mongo DB
        public JsonResult Post(User user)
        {
            int LastUserId = mongo.database.GetCollection<User>("User").AsQueryable().Count();
            user.userId = LastUserId + 1;

            mongo.database.GetCollection<User>("User").InsertOne(user);
            return new JsonResult("Registrado Correctamente");
        }

        [HttpDelete("{id}")]
        //Add user to mongo DB
        public JsonResult Delete(string id)
        {
            return new JsonResult(collection.deleteUser(id));
        }
    }
}
