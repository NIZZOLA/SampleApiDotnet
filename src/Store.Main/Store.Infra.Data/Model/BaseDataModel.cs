using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Store.Infra.Data.Model;

public class BaseDataModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
}