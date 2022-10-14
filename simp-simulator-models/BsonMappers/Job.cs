using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace simp_simulator_models.BsonMappers;

public class Job
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    //[BsonElement("Name")]
    public string Name { get; set; } = null!;

    public int Pay { get; set; } = 0;

    public string SocialStatus { get; set; } = "Less than Negative";
}
