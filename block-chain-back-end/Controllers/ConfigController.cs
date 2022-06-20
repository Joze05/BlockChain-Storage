using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using API1.Models;

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

            int LastConfigId = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Config>("Configuration").AsQueryable().Count();
            config.configId = LastConfigId + 1;

            dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Config>("Configuration").InsertOne(config);

            return new JsonResult("Configuracion agregada correctamente");
        }

        [HttpGet]
        //Get all users from mongo DB
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            var dbList = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Config>("Configuration").AsQueryable();

            return new JsonResult(dbList);
        }

    }
}
