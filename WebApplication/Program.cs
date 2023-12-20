using MongoDB.Driver;
using resumeController;
using DBClient.DataBase;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using DBService.DataBase;
using educationBuilder.Collections;
using resumeController.Controllers;
using EducationModel.Models;
// Configure User Secrets
var secretBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>();

        var configuration = secretBuilder.Build();
        string mongoDBConnectionURI = configuration["mongoDBConnectionUri"];
        string mongoDBName = configuration["mongoDBDatabaseName"];

        // Initialize Clients
        MongoDBClient client = new(mongoDBConnectionURI, mongoDBName);
        MongoClient dbClient = client.dbClient;
        IMongoDatabase resume = dbClient.GetDatabase("Resume");
        await client.CreateCollections();
        EducationBuilder educationBuild= new EducationBuilder(resume);
        await educationBuild.SetEducationCollection();


        Console.WriteLine("The list of collections in this database is: ");

        var collections = resume.GetCollection<BsonDocument>("Education");
        var edu = collections.Find(new BsonDocument()).ToList<BsonDocument>();
        foreach (BsonDocument collection in edu)
        {
            Console.WriteLine(collection);
        }
       
 var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings")); // This line loads MongoDB settings from configuration
builder.Services.AddSingleton<MongoDBService, MongoDBService>(); // This line registers MongoDBService with the loaded settings

builder.Services.AddSingleton<ResumeController>();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

