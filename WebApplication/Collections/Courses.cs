using CourseModel.Models;
using MongoDB.Driver;

using MongoDB.Bson;
using System.Globalization;

namespace courseBuilder.Collections;

public class CourseBuilder
{
    private readonly IMongoDatabase db;
    public IMongoCollection<Course> collection;

    public CourseBuilder(IMongoDatabase db)
    {
        this.db = db;
        this.collection = db.GetCollection<Course>("Courses");

        var indexExists = collection.Indexes.List().ToList().Any(idx => idx["name"] == "IdIndex");
        if (!indexExists)
        {
            var indexKeys = Builders<Course>.IndexKeys.Ascending(a => a.Id);
            var indexOptions = new CreateIndexOptions { Name = "IdIndex" };
            var indexModel = new CreateIndexModel<Course>(indexKeys, indexOptions);
            var indexName = collection.Indexes.CreateOne(indexModel);
        }
    }

    public async Task SetCoursesCollection()
    {

        Course cps616 = new Course
        {
            Id = 1,
            Title = "Algorithms",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 616",
            CourseWork = "N/A"
        };

        Course cps721 = new Course
        {
            Id = 2,
            Title = "Artifical Intelligence I",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 721",
            CourseWork = "N/A"
        };

        Course cps870 = new Course
        {
            Id = 3,
            Title = "Applied Natural Language Processing",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 870",
            CourseWork = ""
        };

        Course mth207 = new Course
        {
            Id = 4,
            Title = "Calculus and Computational Methods I",
            Institution = "Toronto Metropolitan University",
            CourseCode = "MTH 207",
            CourseWork = "N/A"
        };

        Course cps506 = new Course
        {
            Id = 5,
            Title = "Comparative Programing Languages",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 506",
            CourseWork = "https://github.com/xvxvdee/Poker_elixir"
        };

        Course cps706 = new Course
        {
            Id = 6,
            Title = "Computer Networks I",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 706",
            CourseWork = "N/A"
        };

        Course cps209 = new Course
        {
            Id = 7,
            Title = "Computer Science I & II",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 109, CPS 209",
            CourseWork = "N/A"
        };

        Course cps633 = new Course
        {
            Id = 8,
            Title = "Computer Security",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 633",
            CourseWork = "N/A"
        };

        Course cps305 = new Course
        {
            Id = 9,
            Title = "Data Structures",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 305",
            CourseWork = "N/A"
        };

        Course cps510 = new Course
        {
            Id = 10,
            Title = "Database Systems I",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 510",
            CourseWork = "N/A"
        };

        Course cps412 = new Course
        {
            Id = 11,
            Title = "Discrete Structures",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 412",
            CourseWork = "N/A"
        };

        Course cps613 = new Course
        {
            Id = 12,
            Title = "Human-Computer Interaction",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 613",
            CourseWork = "N/A"
        };

        Course CPS842 = new Course
        {
            Id = 13,
            Title = "Information Retrieval and Web Search",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 842",
            CourseWork = "https://github.com/xvxvdee/CPS842-A1"
        };
        Course cps406 = new Course
        {
            Id = 14,
            Title = "Intro to Software Engineering",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 406",
            CourseWork = "https://github.com/maryam-elbeshbishy/QIY"
        };

        Course cps393 = new Course
        {
            Id = 15,
            Title = "Introduction to UNIX, C and C++",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 393",
            CourseWork = "N/A"
        };

        Course mth108 = new Course
        {
            Id = 16,
            Title = "Linear Algebra",
            Institution = "Toronto Metropolitan University",
            CourseCode = "MTH 108",
            CourseWork = "N/A"
        };

        Course cps803 = new Course
        {
            Id = 17,
            Title = "Machine Learning",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 803",
            CourseWork = "https://github.com/xvxvdee/CPS803-FinalProject"
        };

        Course cps590 = new Course
        {
            Id = 18,
            Title = "Operating Systems I",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 590",
            CourseWork = "N/A"
        };

        Course mth380 = new Course
        {
            Id = 19,
            Title = "Probability and Statistics I",
            Institution = "Toronto Metropolitan University",
            CourseCode = "MTH 380",
            CourseWork = "N/A"
        };

        Course cps714 = new Course
        {
            Id = 20,
            Title = "Project Management",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 714",
            CourseWork = "https://github.com/maryam-elbeshbishy/TMUPrep"
        };
        Course cps847 = new Course
        {
            Id = 21,
            Title = "Software Tools for Startups",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 847",
            CourseWork = ""
        };
        Course cps521 = new Course
        {
            Id = 22,
            Title = "Introduction to Data Science",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 521",
            CourseWork = "https://github.com/vanessah9/CPS521/tree/main/Deandra"
        };
        Course soc880 = new Course
        {
            Id = 23,
            Title = "Information Tech and Society",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CSOC 880",
            CourseWork = "N/A"
        };
        Course french = new Course
        {
            Id = 24,
            Title = "French Minor",
            Institution = "Toronto Metropolitan University",
            CourseCode = "FRE 301, FRE 401, FRE 501, FRE 601, FRE 515, FRE 507, FRE 502",
            CourseWork = "Speaking/Writing, Translation I, Business French"
        };
        Course cps844 = new Course
        {
            Id = 25,
            Title = "Data Mining",
            Institution = "Toronto Metropolitan University",
            CourseCode = "CPS 844",
            CourseWork = "https://github.com/xvxvdee/CPS844_Assignment"
        };


        try
        {
            await this.collection.InsertManyAsync(new List<Course>([cps616, cps721, cps870, mth207, cps506, cps706, cps209, cps633, cps305, cps510, cps412, cps613, CPS842, cps406, cps393, mth108, cps803, cps590, mth380, cps714, cps847, cps521, soc880, french, cps844]));
            System.Console.WriteLine("Items have been ingested into the Courses Collection.");

        }
        catch (MongoBulkWriteException)
        {
            System.Console.WriteLine("... Courses Collection is set already.");
        }
        catch (InvalidCastException e)
        {
            System.Console.WriteLine($"InvalidCastException: {e.Message}");
        }
    }
}



