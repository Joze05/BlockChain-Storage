using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;


namespace API1.Models
{
    public class FileM
    {
        
        // public ObjectId _id { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
        public string fileDate {get;set;}
        public double size { get; set; }
        public byte[] fileContent { get; set; }
        public string nombreUsuario {get; set; }

    }
}
