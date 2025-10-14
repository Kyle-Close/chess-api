namespace Chess;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

public class Mongo
{
    public string ConnectionURI { get; set; }

    public Mongo(string connectionURI)
    {
        ConnectionURI = connectionURI;

        var settings = MongoClientSettings.FromConnectionString(ConnectionURI);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        if (!BsonClassMap.IsClassMapRegistered(typeof(Piece)))
        {
            BsonClassMap.RegisterClassMap<Piece>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.SetDiscriminator(nameof(Piece));
            });

            BsonClassMap.RegisterClassMap<Pawn>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<Rook>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<Bishop>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<Knight>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<Queen>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<King>(cm => cm.AutoMap());
        }

        // Create a new client and connect to the server
        var client = new MongoClient(settings);

        // Send a ping to confirm a successful connection
        try
        {
            var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private IMongoDatabase GetDB(string name)
    {
        try
        {
            var client = new MongoClient(ConnectionURI);

            return client.GetDatabase(name);
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting DB: " + ex.Data);
        }
    }

    public async void CreateActiveGame(Game activeGame)
    {
        try
        {
            var db = GetDB("Chess");

            var activeGamesCollection = db.GetCollection<Game>("Active Games");
            await activeGamesCollection.InsertOneAsync(activeGame);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Game> GetActiveGame(string id)
    {
        try
        {
            var db = GetDB("Chess");
            var activeGamesCollection = db.GetCollection<Game>("Active Games");

            var filter = Builders<Game>.Filter.Eq(g => g.Id, id);
            var activeGame = await activeGamesCollection.Find(filter).FirstOrDefaultAsync();

            if (activeGame == null)
            {
                throw new Exception("Could not find active game");
            }

            return activeGame;
        }
        catch
        {
            throw;
        }
    }

    public async Task<ReplaceOneResult> UpdateActiveGame(Game activeGame)
    {
        try
        {
            var db = GetDB("Chess");

            var activeGamesCollection = db.GetCollection<Game>("Active Games");
            var filter = Builders<Game>.Filter.Eq(g => g.Id, activeGame.Id);

            return await activeGamesCollection.ReplaceOneAsync(filter, activeGame);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
