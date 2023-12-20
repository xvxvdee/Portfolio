using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DBClient.DataBase;
using Microsoft.AspNetCore.Mvc;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using DBService.DataBase;
using Microsoft.Extensions.Options;
namespace resumeController.Controllers;

[Route("deandra_spike-madden")]
[ApiController]
public class ResumeController : ControllerBase
{
    private readonly IMongoCollection<BsonDocument> educationCollection;

    public ResumeController(IOptions<MongoDBSettings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);
        var db = mongoClient.GetDatabase(
            dbSettings.Value.DatabaseName);
        this.educationCollection = db.GetCollection<BsonDocument>("Education");
    }

    [HttpGet]
    public ActionResult<string> HomePage()
    {
        return Ok("Welcome to my Resume!");
    }

    [HttpGet("about")]
    public ActionResult<string> About()
    {
        return Ok("Hi! My name is...");
    }

    [HttpGet("contact")]
    public ActionResult<string> Contact()
    {
        return Ok("List of contact information");
    }

    [HttpGet("resume/education")]
    public ActionResult<string> Education()
    {
        var documents = this.educationCollection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return Ok(json);
    }

    [HttpGet("resume/education/courses")]
    public ActionResult<string> Courses()
    {
        return Ok("List course information");
    }

    [HttpGet("resume/experience")]
    public ActionResult<string> Experience()
    {
        return Ok("List Experience");
    }
    [HttpGet("resume/projects")]
    public ActionResult<string> Projects()
    {
        return Ok("List projects");
    }

    [HttpGet("resume/certificates")]
    public ActionResult<string> Certificates()
    {
        return Ok("List certificates");
    }

    [HttpGet("resume/volunteer")]
    public ActionResult<string> Volunteer()
    {
        return Ok("List volunteer time");
    }

}
