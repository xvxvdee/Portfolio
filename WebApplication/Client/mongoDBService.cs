using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using EducationModel.Models;
using ExperienceModel.Models;

using educationBuilder.Collections;
using experienceBuilder.Collections;
using projectBuilder.Collections;
using courseBuilder.Collections;
using volunteerBuilder.Collections;
using certificateBuilder.Collections;

namespace DBService.DataBase;
public class MongoDBService
{
    private EducationBuilder educationCollection;
    private ExperienceBuilder experienceCollection;
    private ProjectBuilder projectCollection;
    private CourseBuilder coursesCollection;
    private VolunteerBuilder volunterCollection;
    private CertificateBuilder certficateCollection;

    public MongoClient mongoClient;
    private IMongoDatabase db;
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
    public string GetEducation()
    {
        var documents = this.educationCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
    public string GetExperience()
    {
        var documents = this.experienceCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
    public string GetProject()
    {
        var documents = this.projectCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }

    public string GetCourses()
    {
        var documents = this.coursesCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
    public string GetVolunteer()
    {
        var documents = this.volunterCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
    public string GetCertificates()
    {
        var documents = this.certficateCollection.collection.Find(new BsonDocument()).ToList();
        var json = documents.ToJson();
        return json;
    }
}