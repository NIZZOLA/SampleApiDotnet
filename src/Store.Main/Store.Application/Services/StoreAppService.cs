using Mapster;
using Store.Application.Interfaces;
using Store.Application.Validators;
using Store.Business.Interfaces;
using Store.Domain.Domain;

namespace Store.Application.Services;
public class StoreAppService : IStoreAppService
{
    private readonly IStoreService _storeService;
    public StoreAppService(IStoreService storeService)
    {
        _storeService = storeService;
    }

    public async Task<IList<StoreResponseModel>> GetAll()
    {
        var objList = await _storeService.GetAll();
        return objList.Adapt<List<StoreResponseModel>>();
    }

    public async Task<StoreResponseModel> GetOne(Guid id)
    {
        var obj = await _storeService.GetOne(id);
        return obj.Adapt<StoreResponseModel>();
    }

    public async Task<Result<StoreResponseModel>> Save(StorePostRequestModel obj)
    {
        var validationResult = new StorePostRequestModelValidator().Validate(obj);
        if(!validationResult.IsValid)
        {
            return Result.Fail(string.Join(",", validationResult.Errors.ToList()));
        }

        var model = obj.Adapt<StoreModel>();
        var result = await _storeService.Save(model);
        if(result.IsFailed) 
        {
            return Result.Fail(ErrorMessageHelpers.CreateErrorMessage("create", Guid.Parse(model.Id), result.Errors));
        }
        return Result.Ok(result.Value.Adapt<StoreResponseModel>());
    }

    public async Task<Result<StoreResponseModel>> Update(Guid id, StorePutRequestModel obj)
    {
        var validationResult = new StorePutRequestModelValidator().Validate(obj);
        if (!validationResult.IsValid)
        {
            return Result.Fail(string.Join(",", validationResult.Errors.ToList()));
        }

        var model = obj.Adapt<StoreModel>();
        var result = await _storeService.Update(model);
        if (result.IsFailed)
        {
            return Result.Fail(ErrorMessageHelpers.CreateErrorMessage("update", id, result.Errors));
        }
        return Result.Ok(result.Value.Adapt<StoreResponseModel>());
    }
    public async Task<Result<bool>> Delete(Guid id)
    {
        var exists = await _storeService.GetOne(id);
        if (exists == null)
            return Result.Fail($"Item not found with id: {id}");

        var result = await _storeService.Delete(id);
        if (result.IsFailed)
            return Result.Fail(ErrorMessageHelpers.CreateErrorMessage("delete", id, result.Errors));

        return Result.Ok(result.Value);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
