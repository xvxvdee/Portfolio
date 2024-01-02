using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using mongoDBSettings.Models;

using educationBuilder.Collections;
using experienceBuilder.Collections;
using projectBuilder.Collections;
using courseBuilder.Collections;
using volunteerBuilder.Collections;
using certificateBuilder.Collections;

using inputToMongoDB.Serivce;

using documentIdNotFoundException.Exceptions;
using companyNotFoundException.Exceptions;
using skillNotFoundException.Exceptions;
using techStackNotFoundException.Exceptions;

namespace DBService.Service;


public class MongoDBService
{
    private readonly EducationBuilder educationCollection;
    private readonly ExperienceBuilder experienceCollection;
    private  readonly ProjectBuilder projectCollection;
    private readonly CourseBuilder coursesCollection;
    private readonly VolunteerBuilder volunterCollection;
    private  readonly CertificateBuilder certficateCollection;
    public MongoClient mongoClient;
    private readonly IMongoDatabase db;
    private readonly InputToMongoDBService inputToMongoDBService = new();
    public MongoDBService(IOptions<MongoDBSettings> dbSettings)//string connectionURI, string dbName)
    {
        this.mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        this.db = this.mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        this.educationCollection = new(this.db);
        this.experienceCollection = new(this.db);
        this.projectCollection = new(this.db);
        this.coursesCollection = new(this.db);
        this.volunterCollection = new(this.db);
        this.certficateCollection = new(this.db);
    }
    public async Task CreateCollections()
    {
        try
        {
            await db.CreateCollectionAsync("Projects");
            await db.CreateCollectionAsync("Experience");
            await db.CreateCollectionAsync("Education");
            await db.CreateCollectionAsync("Volunteer");
            await db.CreateCollectionAsync("Certificates");
            await db.CreateCollectionAsync("Courses");
        }
        catch (MongoDB.Driver.MongoCommandException)
        {
            System.Console.WriteLine("Collections exist in database already.");
        }
    }

    public async Task SetUpCollections()
    {
        await this.educationCollection.SetEducationCollection();
        await this.experienceCollection.SetExperienceCollection();
        await this.projectCollection.SetProjectCollection();
        await this.coursesCollection.SetCoursesCollection();
        await this.volunterCollection.SetVolunteerCollection();
        await this.certficateCollection.SetCertificateCollection();
    }

    // Education Collection
    public string GetEducation()
    {
        var documents = this.educationCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
    public async Task<string> GetEducation(int id) // Return document based on Id
    {
        var size = await this.educationCollection.GetSize();
        var validation = this.inputToMongoDBService.CheckIdRange(id, size);
        if (validation)
        {
            var document = await this.educationCollection.collection.Find(educationCollection => educationCollection.Id == id).FirstOrDefaultAsync();
            var json = document.ToJson();
            return json;
        }
        else
        {
            throw new DocumentIdNotFoundException();
        }
    }

    // Experience Collection
    public string GetExperience()
    {
        var documents = this.experienceCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
    public async Task<string> GetExperience(int id) // Return document based on Id
    {
        var size = await this.experienceCollection.GetSize();
        var validation = this.inputToMongoDBService.CheckIdRange(id, size);
        if (validation)
        {
            var document = await this.experienceCollection.collection.Find(experienceCollection => experienceCollection.Id == id).FirstOrDefaultAsync();
            var json = document.ToJson();
            return json;
            throw new DocumentIdNotFoundException();
        }
        else
        {
            throw new DocumentIdNotFoundException();
        }
    }
    public async Task<string> GetExperienceByCompany(string company)// Return document based on company
    {
        List<string> companies = await this.experienceCollection.GetCompanies();
        var validation = this.inputToMongoDBService.CheckInputExistsProperty(companies, company);
        if (validation)
        {
            var input = this.inputToMongoDBService.SerializeFragmentIdentifiers(company);
            var documents = await this.experienceCollection.collection.Find(experienceCollection => experienceCollection.Company == input).ToListAsync();
            var json = documents.ToJson();
            return json;
        }
        else
        {
            throw new CompanyNotFoundException();
        }
    }
    public async Task<string> GetExperienceBySkill(string skill) //Return document based on skill
    {
        List<string> skills = await this.experienceCollection.GetSkills();
        var validation = this.inputToMongoDBService.CheckInputExistsProperty(skills, skill);
        if (validation)
        {
            var input = this.inputToMongoDBService.SerializeFragmentIdentifiers(skill);
            var documents = this.experienceCollection.collection.Find(experienceCollection => experienceCollection.SkillsDeveloped.Any(skillsDevelop => skillsDevelop.Contains(input, StringComparison.CurrentCultureIgnoreCase))).ToList();
            var json = documents.ToJson();
            return json;
        }
        else
        {
            throw new SkillNotFoundException();
        }
    }

    // Project Collection
    public string GetProject()
    {
        var documents = this.projectCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }

    public async Task<string> GetProject(int id) // Return document based on Id
    {
        var size = await this.projectCollection.GetSize();
        var validation = this.inputToMongoDBService.CheckIdRange(id, size);
        if (validation)
        {
            throw new DocumentIdNotFoundException();
        }
        else
        {
            var document = await this.projectCollection.collection.Find(projectCollection => projectCollection.Id == id).FirstOrDefaultAsync();
            var json = document.ToJson();
            return json;
        }
    }

    public async Task<string> GetProject(string techStack) // Return document based on techstack
    {
        List<string> entireTechStack = await this.projectCollection.GetTechStack();
        var validation = this.inputToMongoDBService.CheckInputExistsProperty(entireTechStack, techStack);
        if (validation)
        {
            var input = this.inputToMongoDBService.SerializeFragmentIdentifiers(techStack);
            var documents = this.projectCollection.collection.Find(projectCollection => projectCollection.TechStack.Any(stack => stack.Contains(input, StringComparison.CurrentCultureIgnoreCase))).ToList();
            var json = documents.ToJson();
            return json;
        }
        else
        {
            throw new TechStackNotFoundException();
        }
    }

    // Courses Collection
    public string GetCourses()
    {
        var documents = this.coursesCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
    // Volunteer Collection
    public string GetVolunteer()
    {
        var documents = this.volunterCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
    // Certificates Collection
    public string GetCertificates()
    {
        var documents = this.certficateCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
}