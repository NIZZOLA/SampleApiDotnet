namespace Store.Infra.Data.MongoDb;

public class MongoDbConfig
{
    public string Name { get; init; }
    public string Host { get; init; }
    public int Port { get; init; }
    public string ConnectionString => $"mongodb://{Host}:{Port}";
}
