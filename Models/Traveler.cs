using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReservationAppApi.Models
;
public class Traveler
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string NIC { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string AccountStatus { get; set; }

    public required string Password { get; set; }
}
