using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API1.Models
{
    public class User
    {
        public ObjectId id { get; set; }

        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string? name { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? birthDay { get; set; }

    }
}
