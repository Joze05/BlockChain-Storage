using API1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using System;


namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public FilesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //private readonly MongoGridFS _gridFs;

        /*[HttpPost]
        public ActionResult Post([FromForm] List<IFormFile> files)
        {
            List<FileM> archivos = new List<FileM>();
            try
            {
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        var id = ObjectId.Empty;
                        using (var fileTemp = System.IO.File.OpenRead(file.FileName))
                        {
                            _gridFs.Upload(file, file.FileName);
                        }
                    }

                    MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

                    //int LastFileId = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<FileM>("Files").AsQueryable().Count();
                    //file.fileId = LastFileId + 1;

                    dbClient.GetDatabase("BlockChainDocsDB").GetCollection<FileM>("Files").InsertMany(archivos);

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(archivos);
        }*/
        [HttpGet]
        //Get all files from mongo DB
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            var dbList = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<FileM>("Files").AsQueryable();

            return new JsonResult(dbList);
        }

        [HttpDelete("{_id}")]
        //Add user to mongo DB
        public JsonResult deleteById(string _id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));
            var dbList = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<FileM>("Files");
            dbList.DeleteOneAsync(x => x._id == _id);
            // var filter = Builders<FileM>.Filter.Eq("_id", id);
            // var singleFile = dbList.Find(filter).FirstOrDefault();

            return new JsonResult("Ok");
        }
        /*
        [HttpPost]
        public ActionResult Post([FromForm] List<IFormFile> files)
        {
            List<FileM> archivos = new List<FileM>();
            try
            {
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        // var filePath = "C:\\Users\\alvar\\Desktop\\Projects\\BlockChainApp\\Blockchain-TS\\block-chain-back-end\\fileStorage\\" + file.FileName;
                        // using (var stream = System.IO.File.Create(filePath))

                        // {
                        //     file.CopyToAsync(stream);
                        // }
                        
                        double size = file.Length;
                        size = size / 1000000;
                        size = Math.Round(size, 2);
                        FileM archivo = new FileM();
                        archivo.extension = Path.GetExtension(file.FileName).Substring(1);
                        archivo.name = Path.GetFileNameWithoutExtension(file.FileName);
                        archivo.size = size;
                        archivo.path = filePath;
                        byte[] binaryContent = System.IO.File.ReadAllBytes(filePath);
                        archivo.fileContent = binaryContent;
                        archivos.Add(archivo);

                    }

                    MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

                    //int LastFileId = dbClient.GetDatabase("BlockChainDocsDB").GetCollection<FileM>("Files").AsQueryable().Count();
                    //file.fileId = LastFileId + 1;

                    dbClient.GetDatabase("BlockChainDocsDB").GetCollection<FileM>("Files").InsertMany(archivos);

                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(archivos);
        }
        */

        [HttpPost("{userName}")]
        public ActionResult PostFile([FromForm] List<IFormFile> files, [FromRoute] string userName)
        {
            Console.WriteLine(userName);
            List<FileM> archivos = new List<FileM>();
            try
            {
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        double size = file.Length;
                        FileM archivo = new FileM();
                        archivo.extension = Path.GetExtension(file.FileName).Substring(1);
                        archivo.name = Path.GetFileNameWithoutExtension(file.FileName);
                        archivo.size = size;
                        archivo.owner = userName;
                        archivo.fileDate = DateTime.Today.ToString("d");
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            byte[] binaryContent = ms.ToArray();
                            archivo.fileContent = binaryContent;
                        }
                        archivos.Add(archivo);
                    }

                    MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

                    dbClient.GetDatabase("BlockChainDocsDB").GetCollection<FileM>("Files").InsertMany(archivos);

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(archivos);
        }



        /*[HttpPost]
        public ActionResult Post([FromForm] FileM<IFormFile> file)
        {
            try { 
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("BlockChainAppCon"));

            byte[] binaryContent = System.IO.File.ReadAllBytes(file.FileName);

            dbClient.GetDatabase("BlockChainDocsDB").GetCollection<FileM>("Files").InsertOne(file);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(file);
        }*/

    }

}
