using MongoDB.Bson;

namespace API1.Models
{
    public class Config
    {
        public ObjectId _id { get; set; }
        public string owner { get; set; }
        public string configName { get; set; }
        public string configValue { get; set; }

    }
}
