using MongoDB.Bson;
using MongoDB.Driver;

namespace API1.Models
{
    public class FileM
    {
        public ObjectId _id { get; set; }
        public string? owner { get; set; }
        public string? name { get; set; }
        public string? extension { get; set; }
        public double? size { get; set; }
        public string? fileContent { get; set; }
        public string? fileDate { get; set; }

    }
}
