using API1.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : Controller
    {
        private readonly IConfiguration _configuration;
        public BlockController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("{owner}")]
        //Add user to mongo DB
        public JsonResult Post(Block block, string owner)
        {
            block.owner = owner;
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));
            //Console.WriteLine();
            try
            {
                dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Block>("Block").InsertOneAsync(block);
                Console.WriteLine("Exito al insertar el bloque");
                return new JsonResult("Exito");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el bloque");
                return new JsonResult("Falló: ", ex);
            }
            // return new JsonResult("no sé");
        }

        [HttpGet("{owner}")]
        //Get all users from mongo DB
        public List<Block> GetOwnerBlocks(string owner)
        {
            List<Block> blockList = new List<Block>();

            try
            {

                MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));
                var filter = Builders<Block>.Filter.Eq("owner", owner);
                blockList = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Block>("Block").Find(filter).ToList();

            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR: " + ex.Message);

            }
            //var hashPrevio = blocks[lastBlock].hashPrevio;

            //Console.WriteLine(blocks);
            return blockList;

        }

        [HttpGet("getLastHash")]
        //Get hash of last block
        public string GetLastHash()
        {
            List<Block> blockList = new List<Block>();
            Block lastBlock = new Block();
            string previewsHash = "";

            try
            {

                MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));
                blockList = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<Block>("Block").AsQueryable().ToList();
                lastBlock = blockList[blockList.Count - 1];
                previewsHash = lastBlock.hash;
                //var  = blockList[totalOfBlocks];

            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR: " + ex.Message);

            }
            //var hashPrevio = blocks[lastBlock].hashPrevio;

            //Console.WriteLine(blocks);
            return previewsHash;

        }
    }
}
