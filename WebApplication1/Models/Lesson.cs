using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class Lesson
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string title { get; set; }

        public string description { get; set; }
         
        public string teacher { get; set; }
        public int nbrH { get; set; }
        public int prix { get; set; }
        public int LessonId { get; internal set; }
    }
}
