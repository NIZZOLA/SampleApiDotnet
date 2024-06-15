using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.Infra.Data.Configuration;
using Store.Infra.Data.Model;

namespace Store.Infra.Data.Context;
public class StoreMongoDbContext : IStoreMongoDbContext
{
    private readonly IMongoDatabase _db;
    private ILogger _logger;
    public StoreMongoDbContext(IOptions<MongoDbConfiguration> config, ILogger<StoreMongoDbContext> logger)
    {
        _logger = logger;
        try
        {
            var mongoClient = new MongoClient(config.Value.ConnectionString);

            _db = mongoClient.GetDatabase(config.Value.DatabaseName);
        }
        catch (Exception error)
        {
            _logger.LogError($"Error connecting database {error.Message} {error.StackTrace}");
            throw;
        }
    }
    public IMongoCollection<StoreDataModel> Stores => _db.GetCollection<StoreDataModel>("Stores");
}
