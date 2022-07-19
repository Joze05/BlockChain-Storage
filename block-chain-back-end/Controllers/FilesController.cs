using API1.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using API1.Database;
using API1.Interfaces;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using API1.Controllers;
using Newtonsoft.Json;


namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private FileMCollection collection;

        public FilesController(IConfiguration configuration)
        {
            collection = new FileMCollection(configuration);
            _configuration = configuration;
        }

        [HttpGet]
        //Get all files from mongo DB
        public JsonResult Get()
        {
            return new JsonResult(collection.getFiles());
        }

        [HttpGet("{_id}")]
        //Add user to mongo DB
        public JsonResult getById(string _id)
        {
            return new JsonResult(collection.getFileById(_id));
        }

        [HttpGet("userName/{userName}")]
        //Add user to mongo DB
        public JsonResult getByUserName(string userName)
        {
            return new JsonResult(collection.getFileByUserName(userName));
        }

        [HttpPost("{usuario}")]
        public ActionResult PostFile([FromForm] List<IFormFile> files, string usuario)
        {
            List<FileM> archivos = new List<FileM>();
            try
            {
                archivos = collection.postFiles(files, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult("OK");
        }

        [HttpDelete]
        //Add user to mongo DB
        public JsonResult Delete(List<JObject> id)
        {
            collection.deleteFile(id);
            return new JsonResult("Eliminado Correctamente");
        }

        [HttpPost("minar")]

        public JsonResult getMinado([FromBody] string owner)
        {


            ConfigController cc = new ConfigController(_configuration);

            Console.WriteLine(owner);

            int filesPerBlock = Int32.Parse(cc.GetOwnerConfig(owner).configValue);

            List<FileM> allOwnerFiles = collection.getFilesObject(owner);
            List<FileM> filesToMining = new List<FileM>();//Se crea la lista con los archivos a minar dependiendo de la cantidad de la configuracion

            if (allOwnerFiles.Count < filesPerBlock)
            {

                foreach (FileM file in allOwnerFiles)
                {
                    filesToMining.Add(file);
                }

            }
            else
            {

                for (int i = 0; i < filesPerBlock; i++)
                {
                    filesToMining.Add(allOwnerFiles[i]);
                }

            }

            if (miningCore(filesToMining, owner))
            {

                return new JsonResult("Exito al minar");

            }
            else
            {

                return new JsonResult("Error al minar");

            };

        }

        public bool miningCore(List<FileM> filesToMining, string owner)
        {
            bool stateOfMining = false;
            BlockController bc = new BlockController(_configuration);

            //-----------------------------------Inicio minado---------------------------------------------------

            Boolean flag = false; //este valor es para indicar que debe salir del metodo porque ya encontró el hash valido

            int prueba = 0;
            var fechaMinado = DateTime.Now.ToString("yyyyMMddHHmmss");
            double milliseconds = 0;
            string hashPrevio = "";
            string hash = "";

            Console.WriteLine("Esta es la fecha en la que inicia el minado: " + fechaMinado);
            while (!flag) //El flag cambia cuando se haya encontrado el hash valido "0000...."
            {

                milliseconds = DateTime.Now.Millisecond;

                if (DateTime.Now.ToString("yyyyMMddHHmmss") != fechaMinado) //Este if es para hacer constar que ya pasó un segundo
                {
                    prueba = 0;
                    fechaMinado = DateTime.Now.ToString("yyyMMddHHmmss");
                }

                hash = getHash(fechaMinado + prueba + filesToMining);

                //Console.WriteLine("FechaMinado: " + fechaMinado + "/ Prueba: " + prueba + "/ Hash: " + hash + "/ Milliseconds: " + milliseconds);

                if (hash.StartsWith("0000"))
                {
                    Console.WriteLine("El hash valido es: " + hash); //retorna el hash valido

                    Block block = new Block();
                    block.fechaMinado = fechaMinado;
                    block.prueba = prueba;
                    block.milliseconds = milliseconds;
                    block.fileList = filesToMining;
                    block.hashPrevio = bc.GetLastHash();
                    block.hash = hash;

                    //Insert block to DB
                    bc.Post(block, owner);
                    collection.deleteFilesMem(filesToMining); //Borra los archivos minados del grid del mempool
                    filesToMining.Clear();

                    stateOfMining = true;
                    flag = true;

                }

                prueba++;

            } //-----------------------------------Fin minado---------------------------------------------------

            return stateOfMining;

        }


        private static string getHash(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }

}
