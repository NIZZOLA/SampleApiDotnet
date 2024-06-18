using Mapster;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Store.Domain.Domain;
using Store.Infra.Data.Context;
using Store.Infra.Data.Interfaces;
using Store.Infra.Data.Model;

namespace Store.Infra.Data.Repositories;
public class StoreRepository : IStoreRepository
{
    private readonly IStoreMongoDbContext _context;
    private readonly ILogger _logger;
    public StoreRepository(IStoreMongoDbContext context, ILogger<StoreRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Create(StoreModel model)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            _logger.LogInformation($"{StoreMessageConstants.CreateProcessStarted} {model.Id}");
        bool result = false;
        try
        {
            var dbModel = model.Adapt<StoreDataModel>();
            await _context.Stores.InsertOneAsync(dbModel);
            result = true;
        }
        catch (Exception error)
        {
            _logger.LogError($"{StoreMessageConstants.CreateError} - {model.Id} Error: {error.Message.ToString()} {error.StackTrace}");
        }
        return result;
    }

    public async Task<bool> Delete(Guid id)
    {
        if (_logger.IsEnabled(LogLevel.Information)) 
            _logger.LogInformation($"{StoreMessageConstants.DeleteProcessStarted} {id}");
        bool result = false;
        try
        {
            var filter = FindById(id.ToString());
            result = _context.Stores.DeleteOneAsync(filter).Result.IsAcknowledged;
        }
        catch (Exception error)
        {
            _logger.LogError($"{StoreMessageConstants.DeleteError} {id} Error: {error.Message.ToString()} {error.StackTrace}");
        }
        return result;
    }

    public async Task<IEnumerable<StoreModel>> GetAll()
    {
        var result = await _context
                        .Stores
                        .Find(_ => true)
                        .ToListAsync();

        return result.Adapt<IEnumerable<StoreModel>>();
    }

    public async Task<StoreModel> GetOne(Guid id)
    {
        var filter = FindById(id.ToString());
        var result = await _context
                .Stores
                .Find(filter)
                .FirstOrDefaultAsync();

        return result.Adapt<StoreModel>();
    }

    private FilterDefinition<StoreDataModel> FindById(string id)
    {
        return Builders<StoreDataModel>.Filter.Eq(m => m.Id, id);
    }

    public async Task<bool> Update(StoreModel model)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            _logger.LogInformation($"{StoreMessageConstants.UpdateProcessStarted} {model.Id}");
        var result = false;
        try
        {
            var dbModel = model.Adapt<StoreDataModel>();

            ReplaceOneResult updateResult =
                await _context
                        .Stores
                        .ReplaceOneAsync(
                            filter: g => g.Id == model.Id,
                            replacement: dbModel);
            result = updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        catch (Exception error)
        {
            _logger.LogError($"{StoreMessageConstants.UpdateError} {model.Id} Error: {error.Message.ToString()} {error.StackTrace}");
        }
        return result;
    }
}
