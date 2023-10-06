using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReservationAppApi.Models;
public class Train
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string TrainName { get; set; }
    public required string Schedule { get; set; }
    public required string Status { get; set; } // Active or Canceled
}