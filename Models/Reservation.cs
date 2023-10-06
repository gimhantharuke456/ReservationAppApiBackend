using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReservationAppApi.Models;

public class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }


    public string? CreatedBy { get; set; }
    public string? CreatedTo { get; set; }
    public DateTime ReservationDate { get; set; }
    public string? Status { get; set; }
}