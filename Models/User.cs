using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace ReservationAppApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("NIC")]
        [BsonRequired]
        public string NIC { get; set; }

        [BsonRequired]
        public string Username { get; set; }

        [BsonRequired]
        public string Password { get; set; }

        [BsonRequired]
        public string Role { get; set; } // Backoffice or Travel Agent
    }

    public static class UserIndexes
    {
        public static void CreateIndexes(IMongoCollection<User> collection)
        {
            // Create a unique index on the NIC field
            var keys = Builders<User>.IndexKeys.Ascending(u => u.NIC);
            var options = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<User>(keys, options);
            collection.Indexes.CreateOne(indexModel);
        }
    }
}
