using CertificateModel.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Globalization;

namespace certificateBuilder.Collections;

public class CertificateBuilder
{
    private readonly IMongoDatabase db;
    public IMongoCollection<Certificate> collection;

    public CertificateBuilder(IMongoDatabase db)
    {
        this.db = db;
        this.collection = db.GetCollection<Certificate>("Certificates");

        var indexExists = collection.Indexes.List().ToList().Any(idx => idx["name"] == "IdIndex");
        if (!indexExists)
        {
            var indexKeys = Builders<Certificate>.IndexKeys.Ascending(a => a.Id);
            var indexOptions = new CreateIndexOptions { Name = "IdIndex" };
            var indexModel = new CreateIndexModel<Certificate>(indexKeys, indexOptions);
            var indexName = collection.Indexes.CreateOne(indexModel);
        }
    }

    public async Task SetCertificateCollection()
    {
        Certificate sqlFilterSort = new Certificate
        {
            Id = 1,
            Title = "Filtering and Sorting Data in SQL Course",
            Issuer = "Dataquest.io",
            DateIssued = DateTime.ParseExact("2023-01", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = "W9SBIZONKT3SNC8IV9BG"
        };

        Certificate sqlIntro = new Certificate
        {
            Id = 2,
            Title = "Introduction to SQL and Databases Course",
            Issuer = "Dataquest.io",
            DateIssued = DateTime.ParseExact("2023-01", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = "IGCJ25MCDQFZ9CPC3M93"
        };

        Certificate dataVizPython = new Certificate
        {
            Id = 3,
            Title = "Introduction to Data Visualization in Python Course",
            Issuer = "Dataquest.io",
            DateIssued = DateTime.ParseExact("2022-12", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = "GF91BKUQ52V4L4W1NH62"
        };

        Certificate pandasNumpy = new Certificate
        {
            Id = 4,
            Title = "Introduction to Pandas and NumPy for Data Analysis Course",
            Issuer = "Dataquest.io",
            DateIssued = DateTime.ParseExact("2022-11", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = "RDKCVLMZEYYAVB6SXDKE"
        };

        Certificate msPlanner = new Certificate
        {
            Id = 5,
            Title = "Microsoft Planner Essential Training",
            Issuer = "LinkedIn",
            DateIssued = DateTime.ParseExact("2021-07", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };
        Certificate learningCSharp = new Certificate
        {
            Id = 6,
            Title = "Learning C#",
            Issuer = "LinkedIn",
            DateIssued = DateTime.ParseExact("2021-05", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };

        Certificate jsEssential = new Certificate
        {
            Id = 7,
            Title = "JavaScript Essential Training",
            Issuer = "LinkedIn",
            DateIssued = DateTime.ParseExact("2020-08", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };

        Certificate excelEssential = new Certificate
        {
            Id = 8,
            Title = "Excel Essential Training (Office 365)",
            Issuer = "LinkedIn",
            DateIssued = DateTime.ParseExact("2020-07", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };

        Certificate htmlEssential = new Certificate
        {
            Id = 9,
            Title = "HTML Essential Training",
            Issuer = "LinkedIn",
            DateIssued = DateTime.ParseExact("2020-07", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };

        Certificate outlookEssential = new Certificate
        {
            Id = 10,
            Title = "Outlook Essential Training (Office 365)",
            Issuer = "LinkedIn",
            DateIssued = DateTime.ParseExact("2020-07", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };

        Certificate listeningSkills = new Certificate
        {
            Id = 11,
            Title = "Improving Your Listening Skills",
            Issuer = "LinkedIn",
            DateIssued = DateTime.ParseExact("2020-06", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };

        Certificate cssEssential = new Certificate
        {
            Id = 12,
            Title = "CSS Essential Training",
            Issuer = "LinkedIn",
            DateIssued = DateTime.ParseExact("2020-01", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };

        Certificate highFivePHCD = new Certificate
        {
            Id = 13,
            Title = "High Five PHCD",
            Issuer = "HIGH FIVE Canada",
            DateIssued = DateTime.ParseExact("2017-05", "yyyy-MM", CultureInfo.InvariantCulture),
            CredentialID = ""
        };

        try
        {
            await this.collection.InsertManyAsync(new List<Certificate>([sqlFilterSort, sqlIntro, dataVizPython, pandasNumpy, msPlanner, learningCSharp, jsEssential, excelEssential, htmlEssential, outlookEssential, listeningSkills, cssEssential, highFivePHCD]));
            System.Console.WriteLine("Items have been ingested into the Certificate Collection.");

        }
        catch (MongoBulkWriteException)
        {
            System.Console.WriteLine("... Certificate Collection is set already.");
        }
        catch (InvalidCastException e)
        {
            System.Console.WriteLine($"InvalidCastException: {e.Message}");
        }
    }
}