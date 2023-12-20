using MongoDB.Driver;
using resumeController;
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
// MongoDBService dbService = new(mongoDBConnectionURI, mongoDBName);
// MongoClient dbClient = dbService.mongoClient;
// IMongoDatabase resume = dbClient.GetDatabase(mongoDBName);
// await dbService.CreateCollections();
// await dbService.SetUpCollections();

// Console.WriteLine("The list of collections in this database is: ");

// var collections = resume.GetCollection<BsonDocument>("Education");
// var edu = collections.Find(new BsonDocument()).ToList<BsonDocument>();
// foreach (BsonDocument collection in edu)
// {
//     Console.WriteLine(collection);
// }

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings")); // This line loads MongoDB settings from configuration
builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

