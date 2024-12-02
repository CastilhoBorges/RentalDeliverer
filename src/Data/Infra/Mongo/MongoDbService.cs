namespace RentalDeliverer.src.Data.Infra.Mongo
{
    public class MongoDbService
    {
        private readonly MongoClient _client;
        private readonly string _databaseName;
        private readonly string _collectionName;

        public MongoDbService(IConfiguration configuration)
        {
            _client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _databaseName = configuration["MongoDB:DatabaseName"];
            _collectionName = configuration["MongoDB:CollectionName"];
        }

        private IMongoCollection<BsonDocument> GetCollection()
        {
            var database = _client.GetDatabase(_databaseName);
            return database.GetCollection<BsonDocument>(_collectionName);
        }

        public async Task SaveNotificationAsync(string eventName, string message)
        {
            var collection = GetCollection();
            var document = new BsonDocument
            {
                { "EventName", eventName },
                { "Message", message },
                { "Timestamp", DateTime.UtcNow }
            };

            await collection.InsertOneAsync(document);
        }

        public async Task<List<BsonDocument>> GetNotificationsAsync()
        {
            var collection = GetCollection();
            return await collection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
