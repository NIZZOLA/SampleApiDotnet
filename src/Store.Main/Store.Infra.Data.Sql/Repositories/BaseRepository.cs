using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Domain.Domain;
using Store.Domain.Interfaces.Repositories;
using Store.Infra.Data.Sql.Context;
using Store.Infra.Data.Sql.Model;

namespace Store.Infra.Data.Sql.Repositories;
public class BaseRepository : IBaseRepository<StoreModel>
{
    private readonly StoreContext _context;
    private readonly ILogger _logger;
    public BaseRepository(StoreContext context, ILogger<BaseRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Create(StoreModel model)
    {
        try
        {
            if (string.IsNullOrEmpty(model.Id))
                model.Id = Guid.NewGuid().ToString();

            var entity = new StoreDataModel
            {
                Id = Guid.Parse(model.Id),
                Name = model.Name,
                Phone = model.Phone,
                Email = model.Email
            };
            await _context.Stores.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating store");
            return false;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            var entity = await _context.Stores.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _context.Stores.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting store");
            return false;
        }
    }

    public async Task<IEnumerable<StoreModel>> GetAll()
    {
        try
        {
            var entities = await _context.Stores.ToListAsync();
            return entities.Select(e => new StoreModel
            {
                Id = e.Id.ToString(),
                Name = e.Name,
                Phone = e.Phone,
                Email = e.Email
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all stores");
            return Enumerable.Empty<StoreModel>();
        }
    }

    public async Task<StoreModel> GetOne(Guid id)
    {
        try
        {
            var entity = await _context.Stores.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            return new StoreModel
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Phone = entity.Phone,
                Email = entity.Email
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting store");
            return null;
        }
    }

    public async Task<bool> Update(StoreModel model)
    {
        try
        {
            var entity = await _context.Stores.FindAsync(Guid.Parse(model.Id));
            if (entity == null)
            {
                return false;
            }
            entity.Name = model.Name;
            entity.Phone = model.Phone;
            entity.Email = model.Email;
            _context.Stores.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating store");
            return false;
        }
    }
}
