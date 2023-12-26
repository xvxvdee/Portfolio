using ProjectModel.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Globalization;

namespace projectBuilder.Collections;

public class ProjectBuilder
{
    private readonly IMongoDatabase db;
    public IMongoCollection<Project> collection;

    public ProjectBuilder(IMongoDatabase db)
    {
        this.db = db;
        this.collection = db.GetCollection<Project>("Projects");

        var indexExists = collection.Indexes.List().ToList().Any(idx => idx["name"] == "IdIndex");
        if (!indexExists)
        {
            var indexKeys = Builders<Project>.IndexKeys.Ascending(a => a.Id);
            var indexOptions = new CreateIndexOptions { Name = "IdIndex" };
            var indexModel = new CreateIndexModel<Project>(indexKeys, indexOptions);
            var indexName = collection.Indexes.CreateOne(indexModel);
        }
    }

    public async Task SetProjectCollection()
    {
        Project windows11BatteryAlert = new Project
        {
            Id = 1,
            Title = "Windows11-Battery Alerts",
            Description = "A Python script that alerts the user when their device’s battery is fully charged. The script can be automated using the task manager to run at regular intervals and check the battery status. This way, the user can be notified when their battery is fully charged without having to constantly check it themselves.",
            Repository = "https://github.com/xvxvdee/Windows11-BatteryAlerts",
            Collaboration = false,
            TechStack = new List<string>(["Python", "win11toast", "Microsoft Task Scheduler"])
        };
        Project transitDiscordBot = new Project
        {
            Id = 2,
            Title = "Transit Discord Bot",
            Description = "A Discord bot that uses Selenium to scrape transit delay information from a website. The bot can be added to a Discord server and configured to provide real-time updates on transit delays. This can help users plan their commutes and avoid delays.",
            Repository = "https://github.com/xvxvdee/Transit-Discord-Bot",
            Collaboration = false,
            TechStack = new List<string>(["Python", "Selenium", "Discord"])
        };
        Project activeRecallNotifier = new Project
        {
            Id = 3,
            Title = "Active Recall Notifier",
            Description = "Uses win11toast to help students learn French vocabulary with active recall. Get notifications with vocabulary words and translations.",
            Repository = "https://github.com/xvxvdee/ActiveRecall-Notifier",
            Collaboration = false,
            TechStack = new List<string>(["Python", "win11toast", "Microsoft Task Scheduler"])
        };

        try
        {
            await this.collection.InsertManyAsync(new List<Project>([windows11BatteryAlert, activeRecallNotifier, transitDiscordBot]));
            System.Console.WriteLine("Items have been ingested into the Projects Collection.");

        }
        catch (MongoBulkWriteException)
        {
            System.Console.WriteLine("... Project Collection is set already.");
        }
        catch (InvalidCastException e)
        {
            System.Console.WriteLine($"InvalidCastException: {e.Message}");
        }
    }
}