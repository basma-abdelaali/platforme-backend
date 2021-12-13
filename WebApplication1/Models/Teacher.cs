using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class Teacher
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public string Specialitie { get; set; }

        public string Image { get; set; }

        public int Phone { get; set; }
        public int TeacherId { get; internal set; }
    }
}
