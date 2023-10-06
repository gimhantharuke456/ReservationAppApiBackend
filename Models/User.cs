using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReservationAppApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string NIC { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; } // Backoffice or Travel Agent
    }




}
