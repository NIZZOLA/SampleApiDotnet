namespace Store.Application.Interfaces;
public interface IStoreAppService
{
    Task<Result<StoreResponseModel>> Save(StorePostRequestModel obj);
    Task<Result<StoreResponseModel>> Update(Guid id, StorePutRequestModel obj);
    Task<Result<bool>> Delete(Guid id);
    Task<StoreResponseModel> GetOne(Guid id);
    Task<IList<StoreResponseModel>> GetAll();

    void Dispose();
}
