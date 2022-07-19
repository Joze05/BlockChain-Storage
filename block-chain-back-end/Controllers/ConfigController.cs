using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using API1.Models;
using Newtonsoft.Json.Linq;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        //Add user to mongo DB
        public JsonResult Post(Config config)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Config>("Configuration").InsertOne(config);

            return new JsonResult("Configuracion agregada correctamente");
        }

        [HttpPost("getOwnerConfig")]
        public Config GetOwnerConfig([FromBody] string owner)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));
            var filter = Builders<Config>.Filter.Eq("owner", owner);
            var singleConfig = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Config>("Configuration").Find(filter).FirstOrDefault();
            //dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Config>("Configuration").Find(filter).FirstOrDefault();
            return singleConfig;
        }

        [HttpGet]
        //Get all users from mongo DB
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            var dbList = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Config>("Configuration").AsQueryable().ToList();


            return new JsonResult(dbList);
        }

        [HttpDelete]
        //Add user to mongo DB
        public JsonResult Delete([FromBody]string owner)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            var filter = Builders<Config>.Filter.Eq("owner", owner);

            dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Config>("Configuration").DeleteMany(filter);
            return new JsonResult("Eliminado Correctamente");
        }

    }
}
