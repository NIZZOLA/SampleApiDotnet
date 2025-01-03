using Store.Domain.Domain;
using Store.Infra.Data.MongoDb.Interfaces;

namespace Store.Infra.Data.Interfaces;
public interface IStoreRepository: IBaseRepository<StoreModel>
{
}
