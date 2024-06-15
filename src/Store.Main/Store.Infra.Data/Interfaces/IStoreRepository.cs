using Store.Domain.Domain;
using Store.Infra.Data.Model;

namespace Store.Infra.Data.Interfaces;
public interface IStoreRepository: IBaseRepository<StoreModel>
{
}
