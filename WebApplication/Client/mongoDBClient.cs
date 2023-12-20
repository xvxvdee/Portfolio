using MongoDB.Driver;
using MongoDB.Bson;
using System;
using CourseModel.Models;
using EducationModel.Models;
using ExperienceModel.Models;
using CertificateModel.Models;
using ProjectModel.Models;
using VolunteerModel.Models;
using System.Globalization;

namespace DBClient.DataBase;

public class MongoDBClient
{
private readonly IMongoDatabase db;
public MongoClient dbClient;

    public MongoDBClient(string connectionURI, string dbName)
    {
        this.dbClient = new MongoClient(connectionURI);
        this.db = dbClient.GetDatabase(dbName);
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
    

}