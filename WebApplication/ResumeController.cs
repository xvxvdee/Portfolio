using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using DBService.Service;

using Microsoft.Extensions.Options;
using EducationModel.Models;

using documentIdNotFoundException.Exceptions;
using companyNotFoundException.Exceptions;
using skillNotFoundException.Exceptions;
using techStackNotFoundException.Exceptions;

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
        try
        {
            var response = await this.dbService.GetEducation(id);
            return Ok(response);
        }
        catch (BsonSerializationException)
        {
            return Problem("Internal server error.");
        }
        catch (DocumentIdNotFoundException ex)
        {
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

    [HttpGet("resume/experience/{id}")]
    public async Task<ActionResult<string>> Experience(int id)
    {
        try
        {
            var response = await this.dbService.GetExperience(id);
            return Ok(response);
        }
        catch (BsonSerializationException ex)
        {
            Console.WriteLine(ex.Message);
            return Problem("Internal server error.");
        }
        catch (DocumentIdNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("resume/experience/skills/{skill}")]
    public async Task<ActionResult<string>> ExperienceBySkill(string skill)
    {
        try
        {
            var response = await this.dbService.GetExperienceBySkill(skill);
            return Ok(response);
        }
        catch (BsonSerializationException ex)
        {
            Console.WriteLine(ex.Message);
            return Problem("Internal server error.");
        }
        catch (SkillNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("resume/experience/company/{company}")]
    public async Task<ActionResult<string>> ExperienceByCompany(string company)
    {
        try
        {
            var response = await this.dbService.GetExperienceByCompany(company);
            return Ok(response);
        }
        catch (BsonSerializationException ex)
        {
            Console.WriteLine(ex.Message);
            return Problem("Internal server error.");
        }
        catch (CompanyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("resume/projects")]
    public ActionResult<string> Projects()
    {
        var response = this.dbService.GetProject();
        return Ok(response);
    }

    [HttpGet("resume/projects/{id}")]
    public async Task<ActionResult<string>> Projects(int id)
    {
        try
        {
            var response = await this.dbService.GetProject(id);
            return Ok(response);
        }
        catch (BsonSerializationException ex)
        {
            Console.WriteLine(ex.Message);
            return Problem("Internal server error.");
        }
        catch (DocumentIdNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("resume/projects/techstack/{techstack}")]
    public async Task<ActionResult<string>> Projects(string techstack)
    {
        try
        {
            var response = await this.dbService.GetProject(techstack);
            return Ok(response);
        }
        catch (BsonSerializationException ex)
        {
            Console.WriteLine(ex.Message);
            return Problem("Internal server error.");
        }
        catch (TechStackNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
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
