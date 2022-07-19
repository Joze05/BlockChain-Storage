using API1.Database;
using API1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using System;
using Newtonsoft.Json.Linq;


namespace API1.Interfaces

{
    class FileMCollection : IFileM
    {
        private MongoRepository mongo;
        private IMongoCollection<FileM> collection;
        public FileMCollection(IConfiguration configuration)
        {
            mongo = new MongoRepository(configuration.GetConnectionString("BlockChainAppCon"));
            collection = mongo.database.GetCollection<FileM>("Files");
        }

        public FileM getFileById(string _id)
        {
            ObjectId id = ObjectId.Parse(_id);
            var filter = Builders<FileM>.Filter.Eq("_id", id);
            return collection.Find(filter).FirstOrDefault();
        }

        public List<FileM> getFiles()
        {
            return mongo.database.GetCollection<FileM>("Files").AsQueryable().ToList();
        }

        public List<FileM> getFilesObject(string owner)
        {
            var filter = Builders<FileM>.Filter.Eq("owner", owner);
            List<FileM> fileList = collection.Find(filter).ToList();
            //var fileList = collection.Find(filter).ToList();
            //fileList;

            return fileList; //collection.Find(filter);
        }

        public void deleteFile(List<JObject> id)
        {
            foreach (var item in id)
            {
                ObjectId _id = ObjectId.Parse(item.GetValue("_id").ToString());
                var filter = Builders<FileM>.Filter.Eq("_id", _id);
                collection.DeleteOne(filter);
            }
        }

        public void deleteFiles(List<JObject> list)
        {
            foreach (var item in list)
            {
                //Console.WriteLine("Hola aquí "+item._id.ToString());
                //ObjectId _id = ObjectId.Parse(item._id.ToString());
                ObjectId _id = ObjectId.Parse(item.GetValue("_id").ToString());
                string owner = item.GetValue("owner").ToString();
                //string owner = item.owner.ToString();
                var filter = Builders<FileM>.Filter.Eq("_id", _id);
                filter &= (Builders<FileM>.Filter.Eq("_id", _id) & Builders<FileM>.Filter.Eq("owner", owner));
                collection.DeleteOne(filter);
            }
        }

        public void deleteFilesMem(List<FileM> list)
        {
            foreach (var item in list)
            {
                //Console.WriteLine("Hola aquí "+item._id.ToString());
                //ObjectId _id = ObjectId.Parse(item._id.ToString());
                ObjectId _id = ObjectId.Parse(item._id.ToString());
                //string owner = item.GetValue("owner").ToString();
                string owner = item.owner.ToString();
                var filter = Builders<FileM>.Filter.Eq("_id", _id);
                filter &= (Builders<FileM>.Filter.Eq("_id", _id) & Builders<FileM>.Filter.Eq("owner", owner));
                collection.DeleteOne(filter);
            }
        }

        public List<FileM> postFiles(List<IFormFile> files, string usuario)
        {
            List<FileM> archivos = new List<FileM>();
            try
            {
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        DateTime thisDay = DateTime.Today;
                        double size = file.Length;
                        FileM archivo = new FileM();
                        archivo.extension = Path.GetExtension(file.FileName).Substring(1);
                        archivo.name = Path.GetFileNameWithoutExtension(file.FileName);
                        archivo.size = size;
                        archivo.fileDate = thisDay.ToString("d");
                        archivo.owner = usuario;
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            byte[] binaryContent = ms.ToArray();
                            String fichero = Convert.ToBase64String(binaryContent);
                            archivo.fileContent = fichero;
                        }

                        archivos.Add(archivo);
                    }
                    collection.InsertMany(archivos);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return archivos;
        }

        public List<FileM> getFileByUserName(string userName)
        {
            var filter = Builders<FileM>.Filter.Eq("owner", userName);
            // mongo.database.GetCollection<FileM>("Files").AsQueryable().ToList();
            return collection.Find(filter).ToList();
        }
    }
}