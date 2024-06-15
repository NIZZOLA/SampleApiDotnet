using MongoDB.Bson.Serialization.Attributes;

namespace Store.Infra.Data.Model;
public class StoreDataModel: BaseDataModel
{
    [BsonElement("Name")]
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
