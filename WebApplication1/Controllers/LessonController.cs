using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LessonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("TeacherAppCon"));
            var dbList = dbClient.GetDatabase("platforme").GetCollection<Lesson>("lesson").AsQueryable();
            return new JsonResult(dbList);
        }
        [HttpPost]
        public JsonResult Post(Lesson les)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("TeacherAppCon"));
            int LastLessonId = dbClient.GetDatabase("platforme").GetCollection<Lesson>("lesson").AsQueryable().Count();
            les.LessonId = LastLessonId + 1;
            dbClient.GetDatabase("platforme").GetCollection<Lesson>("lesson").InsertOne(les);
            return new JsonResult("Added successfully");

        }
        [HttpPut]
        public JsonResult Put(Lesson les)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("TeacherAppCon"));

            var filter = Builders<Lesson>.Filter.Eq("LessonId", les.LessonId);
            var update = Builders<Lesson>.Update.Set("title", les.title).Set("description", les.description)
                .Set("nbrH", les.nbrH).Set("prix", les.prix).Set("teacher", les.teacher);
            dbClient.GetDatabase("platforme").GetCollection<Lesson>("lesson").UpdateOne(filter, update);
            return new JsonResult("updated successfully");

        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("TeacherAppCon"));

            var filter = Builders<Lesson>.Filter.Eq("LessonId",id);
            
            dbClient.GetDatabase("platforme").GetCollection<Lesson>("lesson").DeleteOne(filter);
            return new JsonResult("deleted successfully");

        }
    }
}
