using FluentResults;
using Microsoft.Extensions.Logging;
using Store.Domain.Validatiors;
using Store.Infra.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System;

namespace Store.Business.Services;
public class StoreService : ServiceBase<StoreModel>, IStoreService
{
    private readonly IStoreRepository _storeRepository;
    
    public StoreService(IStoreRepository storeRepository, ILogger<StoreService> _logger)
    {
        _storeRepository = storeRepository;
    }

    public override async Task<Result<bool>> Delete(Guid id)
    {
        return await _storeRepository.Delete(id);
    }

    public override async Task<IEnumerable<StoreModel>> GetAll()
    {
        return await _storeRepository.GetAll();
    }


    public override async Task<StoreModel> GetOne(Guid id)
    {
        return await _storeRepository.GetOne(id);
    }

    public override async Task<Result<StoreModel>> Save(StoreModel obj)
    {
        if (obj.Id is null)
            obj.Id = Guid.NewGuid().ToString();

        var businessValidationResult = new StoreModelValidator().Validate(obj);
        if(!businessValidationResult.IsValid) 
            return Result.Fail(string.Join(",", businessValidationResult.Errors.ToList()));
        
        await _storeRepository.Create(obj);
        return Result.Ok(obj);
    }

    public override async Task<Result<StoreModel>> Update(StoreModel obj)
    {
        await _storeRepository.Update(obj);
        return Result.Ok(obj);
    }
}
