using MongoDB.Driver;
using Store.Infra.Data.MongoDb.Model;

namespace Store.Infra.Data.MongoDb.Context;
public interface IStoreMongoDbContext
{
    IMongoCollection<StoreDataModel> Stores { get; }
}