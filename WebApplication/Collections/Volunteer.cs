using VolunteerModel.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Globalization;

namespace volunteerBuilder.Collections;

public class VolunteerBuilder
{
    private readonly IMongoDatabase db;
    public IMongoCollection<Volunteer> collection;

    public VolunteerBuilder(IMongoDatabase db)
    {
        this.db = db;
        this.collection = db.GetCollection<Volunteer>("Volunteer");

        var indexExists = collection.Indexes.List().ToList().Any(idx => idx["name"] == "IdIndex");
        if (!indexExists)
        {
            var indexKeys = Builders<Volunteer>.IndexKeys.Ascending(a => a.Id);
            var indexOptions = new CreateIndexOptions { Name = "IdIndex" };
            var indexModel = new CreateIndexModel<Volunteer>(indexKeys, indexOptions);
            var indexName = collection.Indexes.CreateOne(indexModel);
        }
    }

    public async Task SetVolunteerCollection()
    {
        Volunteer vpCommunications = new Volunteer
        {
            Id = 1,
            Position = "VP Communications & Corporate Relations",
            Company = "Women in Computer Science - Toronto Metropolitan University",
            Field = "Science and Technology",
            Description = "Managed the WiCS email and worked with various teams to send out the WiCS monthly eNewsletter and communicate with the Computer Science Department for funding.",
            StartDate = DateTime.ParseExact("2023-05", "yyyy-MM", CultureInfo.InvariantCulture),
            EndDate = DateTime.MaxValue,
            Responsibilities = new List<string>
            {
                "Managing the WiCS email (quickly responding to emails from Toronto Metropolitan University groups, companies, sponsors and partners, forwarding and CCing members in emails that pertain to them)",
                "Responsible for working with VP marketing, VP events, Director of Graphics and Photographers to send out WiCS monthly eNewsletter",
                "Working with Vice President Finance to communicate with Computer Science Department and Toronto Metropolitan University Science Society for funding"
            }
        };

        Volunteer vpAcademics = new Volunteer
        {
            Id = 2,
            Position = "Vice President of Academics",
            Company = "Women in Computer Science - Toronto Metropolitan University",
            Field = "Science and Technology",
            Description = "Researched and organized academic opportunities and handled diversity and equity issues.",
            StartDate = DateTime.ParseExact("2022-05", "yyyy-MM", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2023-04", "yyyy-MM", CultureInfo.InvariantCulture),
            Responsibilities = new List<string>
            {
                "Researching conferences, scholarships, competitions/hackathons",
                "Organizing career fairs, guest speakers, professional talks",
                "Working with the Computer Science department for outreach opportunities",
                "Handling diversity and equity issues"
            }
        };

        Volunteer president = new Volunteer
        {
            Id = 3,
            Position = "President",
            Company = "Women in Computer Science - Toronto Metropolitan University",
            Field = "Science and Technology",
            Description = "Delegated tasks to executive members, organized tasks and projects, and chaired all meetings of the organization.",
            StartDate = DateTime.ParseExact("2021-05", "yyyy-MM", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2022-05", "yyyy-MM", CultureInfo.InvariantCulture),
            Responsibilities = new List<string>
            {
                "Delegating tasks to respective executive members",
                "Organizing all tasks and projects",
                "Calling and chairing all meetings of the organization"
            }
        };

        Volunteer ryersonDSC = new Volunteer
        {
            Id = 4,
            Position = "Marketing Lead",
            Company = "Ryerson Developer Student Club",
            Field = "Science and Technology",
            Description = "Organized and helped VP Marketing, assisted with logistics of Marketing, and handled the design of event posters and social media posts.",
            StartDate = DateTime.ParseExact("2020-09", "yyyy-MM", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2021-05", "yyyy-MM", CultureInfo.InvariantCulture),
            Responsibilities = new List<string>
            {
                "Organizing and helping VP Marketing",
                "Assisting with logistics of Marketing (getting necessary contacts, creating prior to term and on the go Marketing Plan)",
                "Assisting in handling the design of event posters, and social media posts"
            }
        };
        Volunteer socialMediaDirector = new Volunteer
        {
            Id = 5,
            Position = "Social Media Director",
            Company = "Women in Computer Science - Toronto Metropolitan University",
            Field = "Science and Technology",
            Description = "Organized a social media plan, managed Slack Workspace, displayed professionally related content, and explained basic coding concepts during workshops.",
            StartDate = DateTime.ParseExact("2020-07", "yyyy-MM", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2021-05", "yyyy-MM", CultureInfo.InvariantCulture),
            Responsibilities = new List<string>
            {
                "Organized social media plan to drive engagements",
                "Setup and managed Slack Workspace",
                "Displayed professionally related content on platforms",
                "Enhanced the efficiency of social media scheduling by introducing tools such as Linktree and Buffer",
                "Explained basic coding concepts during workshops"
            }
        };

        Volunteer secondYearRep = new Volunteer
        {
            Id = 6,
            Position = "2nd Year Representative",
            Company = "Women in Computer Science - Toronto Metropolitan University",
            Field = "Science and Technology",
            Description = "Communicated with 2nd year students and helped market and plan events.",
            StartDate = DateTime.ParseExact("2020-07", "yyyy-MM", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2021-05", "yyyy-MM", CultureInfo.InvariantCulture),
            Responsibilities = new List<string>
            {
                "Communicating with 2nd year students in Computer Science and presenting what they want/need from WiCS",
                "Helped market and plan events that bring in more 2nd year students"
            }
        };

        Volunteer cityOfMarkham = new Volunteer
        {
            Id = 7,
            Position = "Volunteer",
            Company = "City of Markham",
            Field = "Children",
            Description = "Assisted counselors with drills, coordinated drills and warm-ups, helped supervise campers, reported any emergencies/accidents to the Program Instructor, and was responsible for cleanup and setup of equipment.",
            StartDate = DateTime.ParseExact("2016-07", "yyyy-MM", CultureInfo.InvariantCulture),
            EndDate = DateTime.ParseExact("2017-08", "yyyy-MM", CultureInfo.InvariantCulture),
            Responsibilities = new List<string>
            {
                "Assisted counselors with drills while creating a fun and safe environment",
                "Coordinated drills and warm-ups",
                "Helped supervise campers",
                "Report any emergencies/accidents to the Program Instructor",
                "Responsible for cleanup and setup of equipment"
            }
        };
        
        try
        {
            await this.collection.InsertManyAsync(new List<Volunteer>([vpCommunications, vpAcademics, president, ryersonDSC, socialMediaDirector, secondYearRep, cityOfMarkham ]));
            System.Console.WriteLine("Items have been ingested into the Volunteer Collection.");

        }
        catch (MongoBulkWriteException)
        {
            System.Console.WriteLine("... Volunteer Collection is set already.");
        }
        catch (InvalidCastException e)
        {
            System.Console.WriteLine($"InvalidCastException: {e.Message}");
        }
    }
}
