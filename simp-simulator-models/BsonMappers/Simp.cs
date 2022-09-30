using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace simp_simulator_models.BsonMappers;

public class Simp
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    //[BsonElement("Name")]
    public string Name { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string IsBanned { get; set; } = "Not yet";
}
