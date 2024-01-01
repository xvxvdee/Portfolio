using MongoDB.Driver;

using ExperienceModel.Models;

using MongoDB.Bson;
using System.Globalization;

namespace experienceBuilder.Collections;

public class ExperienceBuilder
{
    private readonly IMongoDatabase db;
    public IMongoCollection<Experience> collection;

    public ExperienceBuilder(IMongoDatabase db)
    {
        this.db = db;
        this.collection = db.GetCollection<Experience>("Experience");

        var indexExists = collection.Indexes.List().ToList().Any(idx => idx["name"] == "IdIndex");
        if (!indexExists)
        {
            var indexKeys = Builders<Experience>.IndexKeys.Ascending(a => a.Id);
            var indexOptions = new CreateIndexOptions { Name = "IdIndex" };
            var indexModel = new CreateIndexModel<Experience>(indexKeys, indexOptions);
            var indexName = collection.Indexes.CreateOne(indexModel);
        }
    }

    public async Task SetExperienceCollection()
    {
        Experience LaunchPop = new()
        {
            Id = 1,
            Position = "Admin",
            Company = "LaunchPop",
            Location = "Remote",
            Type = "Co-op",
            Description = "I was tasked with designing floor plans and creating social media strategies to increase user engagement. Additionally, I was responsible for generating and posting content on various blogs, including Twitter, Instagram, Shopify, and LinkedIn.",
            StartDate = DateTime.ParseExact("2018-06-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2019-08-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            SkillsDeveloped = new List<string>(["Media Planning", "Social Media Marketing"])
        };
        Experience ExploreIntern = new()
        {
            Id = 2,
            Position = "Explore Intern (SWE & PM)",
            Company = "Microsoft",
            Location = "Remote",
            Type = "Internship",
            Description = "Worked in a group of 3 to solve problems within M365 Substrate.",
            StartDate = DateTime.ParseExact("2021-05-28", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2021-08-13", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            SkillsDeveloped = new List<string>(["Microsoft Power BI", "Microsoft Word", "Microsoft PowerPoint", "C#", "U-SQL", "Visual Studio"])
        };

        Experience SWEIntern1 = new()
        {
            Id = 3,
            Position = "Software Engineer",
            Company = "Microsoft",
            Location = "Redmond, Washington, United States",
            Type = "Internship",
            Description = "Worked to solve problems within M365 Substrate.",
            StartDate = DateTime.ParseExact("2022-06-13", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2022-09-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            SkillsDeveloped = new List<string>(["Microsoft Power BI", "Microsoft Word", "Microsoft PowerPoint", "C#", "U-SQL", "Visual Studio", "Monitoring and Alerting"])
        };
        Experience SWEIntern2 = new()
        {
            Id = 4,
            Position = "Software Engineer",
            Company = "Microsoft",
            Location = "Redmond, Washington, United States",
            Type = "Internship",
            Description = "Worked to solve problems within M365 Substrate.",
            StartDate = DateTime.ParseExact("2023-05-28", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2023-08-18", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            SkillsDeveloped = new List<string>(["Microsoft Graph Connector", "Postman API", "C#", "Microsoft Azure", "Visual Studio"])
        };

        try
        {
            await this.collection.InsertManyAsync(new List<Experience>([LaunchPop, ExploreIntern, SWEIntern1, SWEIntern2]));
            System.Console.WriteLine("Items have been ingested into the Experience Collection.");

        }
        catch (MongoBulkWriteException)
        {
            System.Console.WriteLine("... Experience Collection is set already.");
        }
        catch (InvalidCastException e)
        {
            System.Console.WriteLine($"InvalidCastException: {e.Message}");
        }
    }
    public async Task<long> GetSize()
    {
        var size = await this.collection.CountDocumentsAsync(_ => true);
        return size;
    }

    public async Task<List<string>> GetCompanies()
    {
        var documents = await this.collection.Find(_ => true).ToListAsync();
        List<string> companies = documents.Select(documents => documents.Company).ToList();
        return companies;
    }
    public async Task<List<string>> GetSkills()
    {
        var documents = await this.collection.Find(_ => true).ToListAsync();
        List<List<string>?> skills = documents.Select(documents => documents.SkillsDeveloped).ToList();
        List<string> skillsFlatten = skills.SelectMany(skill => skill).ToList();
        return skillsFlatten.Distinct().ToList();
    }


}