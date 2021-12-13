using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TeacherController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("TeacherAppCon"));
            var dbList = dbClient.GetDatabase("platforme").GetCollection<Teacher>("teacher").AsQueryable();
            return new JsonResult(dbList);
        }
        [HttpPost]
        public JsonResult Post(Teacher tea)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("TeacherAppCon"));
            int LastTeacherId = dbClient.GetDatabase("platforme").GetCollection<Lesson>("teacher").AsQueryable().Count();
            tea.TeacherId = LastTeacherId + 1;
            dbClient.GetDatabase("platforme").GetCollection<Teacher>("teacher").InsertOne(tea);
            return new JsonResult("Added successfully");

        }
        [HttpPut]
        public JsonResult Put(Teacher tea)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("TeacherAppCon"));

            var filter = Builders<Teacher>.Filter.Eq("TeacherId", tea.TeacherId);
            var update = Builders<Teacher>.Update.Set("FirstName", tea.FirstName).Set("LastName", tea.LastName).
                Set("Phone", tea.Phone).Set("Specialitie", tea.Specialitie).Set("Email", tea.Email).
                Set("Image", tea.Image);


            dbClient.GetDatabase("platforme").GetCollection<Teacher>("teacher").UpdateOne(filter, update); 
            return new JsonResult("updated successfully");

        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("TeacherAppCon"));

            var filter = Builders<Teacher>.Filter.Eq("TeacherId", id);

            dbClient.GetDatabase("platforme").GetCollection<Teacher>("teacher").DeleteOne(filter);
            return new JsonResult("deleted successfully");

        }
    }
}
