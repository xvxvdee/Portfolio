using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using DBService.DataBase;
using Microsoft.Extensions.Options;
using EducationModel.Models;

using documentIdNotFoundException.Exceptions;

namespace resumeController.Controllers;


[Route("deandra_spike-madden")]
[ApiController]
public class ResumeController : ControllerBase
{
    private readonly IMongoCollection<Education> educationCollection;
    private MongoDBService dbService;

    public ResumeController(IOptions<MongoDBSettings> dbSettings)
    {
        this.dbService = new(dbSettings);
    }

    [HttpGet]
    public ActionResult<string> HomePage()
    {
        return Ok("Welcome to my Resume!");
    }

    [HttpGet("setup")]
    public async Task<ActionResult<string>> Setup()
    {
        try
        {
            await this.dbService.SetUpCollections();
            return Ok("Database is loaded.");
        }
        catch (Exception e)
        {
            return BadRequest($"Inserts into the database failed: {e}");
        }
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
        var response = this.dbService.GetEducation();
        return Ok(response);
    }

    [HttpGet("resume/education/{id}")]
    public async Task<ActionResult<string>> SpecificEducation(int id)
    {
        try{
        var response = await this.dbService.GetEducation(id);
        return response;
        }
        catch (BsonSerializationException)
        {
            return Problem("Internal server error.");
        }
        catch (DocumentIdNotFoundException ex){
            return NotFound(ex.Message);
        }
    }


    [HttpGet("resume/education/courses")]
    public ActionResult<string> Courses()
    {
        var response = this.dbService.GetCourses();
        return Ok(response);
    }

    [HttpGet("resume/experience")]
    public ActionResult<string> Experience()
    {
        var response = this.dbService.GetExperience();
        return Ok(response);
    }
    [HttpGet("resume/projects")]
    public ActionResult<string> Projects()
    {
        var response = this.dbService.GetProject();
        return Ok(response);
    }

    [HttpGet("resume/certificates")]
    public ActionResult<string> Certificates()
    {
        var response = this.dbService.GetCertificates();
        return Ok(response);
    }

    [HttpGet("resume/volunteer")]
    public ActionResult<string> Volunteer()
    {
        var response = this.dbService.GetVolunteer();
        return Ok(response);
    }

}
