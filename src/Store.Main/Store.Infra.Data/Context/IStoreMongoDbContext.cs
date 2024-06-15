using MongoDB.Driver;
using Store.Infra.Data.Model;

namespace Store.Infra.Data.Context;
public interface IStoreMongoDbContext
{
    IMongoCollection<StoreDataModel> Stores { get; }
}