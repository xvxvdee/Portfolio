using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;


namespace DBService.DataBase;
public class MongoDBService{
private IMongoCollection<BsonDocument> educationCollection;

    public MongoDBService(IOptions<MongoDBSettings> dbSettings){
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);
        var db = mongoClient.GetDatabase(
            dbSettings.Value.DatabaseName);
        this.educationCollection = db.GetCollection<BsonDocument>("Education");
    }

    public List<BsonDocument> GetEducation(){
        var documents =  this.educationCollection.Find(new BsonDocument()).ToList<BsonDocument>();
        return documents;
    }
}