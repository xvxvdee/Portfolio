using EducationModel.Models;
using DBClient.DataBase;
using MongoDB.Driver;

using ExperienceModel.Models;

using MongoDB.Bson;
using System.Globalization;

namespace educationBuilder.Collections;

public class EducationBuilder
{
    private readonly IMongoDatabase db;
    private IMongoCollection<Education> education;

    public EducationBuilder(IMongoDatabase db)
    {
        this.db = db;
        this.education = db.GetCollection<Education>("Education");

        var indexExists = education.Indexes.List().ToList().Any(idx => idx["name"] == "IdIndex");
        if (!indexExists)
        {
            var indexKeys = Builders<Education>.IndexKeys.Ascending(a => a.Id);
            var indexOptions = new CreateIndexOptions { Unique = true, Name = "IdIndex" };
            var indexModel = new CreateIndexModel<Education>(indexKeys, indexOptions);
            var indexName = education.Indexes.CreateOne(indexModel);
        }
    }

    public async Task SetEducationCollection()
    {
        var educationCollection = this.db.GetCollection<Education>("Education");

        Education university = new()
        {
            Id = 2,
            School = "Toronto Metropolitan University",
            Program = "Computer Science",
            Degree = "Bachelors of Science - BS",
            StartDate = DateTime.ParseExact("2019-09-06", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2024-06-18", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            Extracurriculars = new List<BsonString>(["Women in Computer Science Club", "Ryerson Student Developer Club (Google)"])
        };

        Education highSchool = new()
        {
            Id = 1,
            School = "Father Micheal McGivney Catholic Academy",
            Program = "High School Diploma",
            Degree = "",
            StartDate = DateTime.ParseExact("2015-09-04", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2019-06-20", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            Extracurriculars = new List<BsonString>(["Peer Mentor Team", "Basketball/Soccer/Flag Football Team"])
        };

        try
        {
            await educationCollection.InsertManyAsync(new List<Education>([university, highSchool]));
        }
        catch (MongoBulkWriteException)
        {
            System.Console.WriteLine("... Education Collection is ready");
        }
        catch (InvalidCastException e)
        {
            System.Console.WriteLine($"InvalidCastException: {e.Message}");
                        System.Console.WriteLine($"Source: {e.Source}");

        }
    }
}


