using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using DBService.Service;
using mongoDBSettings.Models;

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
        return Ok("Hello! My name is Deandra. I graduated from Toronto Metropolitan University (Toronto, Canada) with a Bachelor of Science in Computer Science and a French minor. Currently, I am working as a Software Engineer at Microsoft. One of my goals is to have a global impact through my work, reaching and influencing people all around the world, which I believe will bring me immense joy and satisfaction. In my free time, I enjoy spending time with friends, learning new languages, and working on side projects. I look forward to future collaborations.");
    }

    [HttpGet("contact")]
    public ActionResult<string> Contact()
    {
        return Ok("Deandra.spikemadden@gmail.com\nhttps://www.linkedin.com/in/deandra-spike-madden/");
    }

    [HttpGet("resume/education")]
    public ActionResult<string> Education()
    {
        try
        {
            var response = this.dbService.GetEducation();
            return Ok(response);
        }
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Education Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Education Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (BsonException ex)
        {
            System.Console.WriteLine($" Target = Education Collection: {ex.Message}");
            return Problem("Internal Service Error.");
        }
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
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Education Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Education Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
    }

    [HttpGet("resume/education/courses")]
    public ActionResult<string> Courses()
    {
        try
        {
            var response = this.dbService.GetCourses();
            return Ok(response);
        }
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Courses Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Courses Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (BsonSerializationException ex)
        {
            System.Console.WriteLine($" Target = Courses Collection: {ex.Message}");
            return Problem("Internal Service Error.");
        }
    }

    [HttpGet("resume/experience")]
    public ActionResult<string> Experience()
    {
        try
        {
            var response = this.dbService.GetExperience();
            return Ok(response);
        }
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (BsonSerializationException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error.");
        }
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
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error");
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
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error");
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
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Experience Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
    }

    [HttpGet("resume/projects")]
    public ActionResult<string> Projects()
    {
        try
        {
            var response = this.dbService.GetProject();
            return Ok(response);
        }
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Project Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Project Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (BsonSerializationException ex)
        {
            System.Console.WriteLine($" Target = Project Collection: {ex.Message}");
            return Problem("Internal Service Error.");
        }

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
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Project Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Project Collection: {ex.Message}");
            return Problem("Internal Service Error");
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
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Projects Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Projects Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }

    }

    [HttpGet("resume/certificates")]
    public ActionResult<string> Certificates()
    {
        try
        {
            var response = this.dbService.GetCertificates();
            return Ok(response);
        }
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Certificates Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Certificates Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (BsonSerializationException ex)
        {
            System.Console.WriteLine($" Target = Certificates Collection: {ex.Message}");
            return Problem("Internal Service Error.");
        }
    }

    [HttpGet("resume/volunteer")]
    public ActionResult<string> Volunteer()
    {
        try
        {
            var response = this.dbService.GetVolunteer();
            return Ok(response);
        }
        catch (NullReferenceException ex)
        {
            System.Console.WriteLine($" Target = Volunteer Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (MongoConnectionException ex)
        {
            System.Console.WriteLine($" Target = Volunteer Collection: {ex.Message}");
            return Problem("Internal Service Error");
        }
        catch (BsonSerializationException ex)
        {
            System.Console.WriteLine($" Target = Volunteer Collection: {ex.Message}");
            return Problem("Internal Service Error.");
        }
    }
}
