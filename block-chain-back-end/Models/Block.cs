using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace API1.Models
{
    public class Block
    {
        public ObjectId Id { get; set; }
        public string fechaMinado { get; set; }
        public int prueba { get; set; }
        public double milliseconds { get; set; }

        public List<FileM> fileList { get; set; }
        public string hashPrevio { get; set; }
        public string hash { get; set; }

        public string owner { get; set; }

    }
}
