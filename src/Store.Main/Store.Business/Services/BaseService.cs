using FluentResults;

namespace Store.Business.Services;
public abstract class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : BaseModel
{
    private bool _disposed = false;
    public abstract Task<Result<TEntity>> Save(TEntity obj);
    public abstract Task<Result<TEntity>> Update(TEntity obj);
    public abstract Task<Result<bool>> Delete(Guid id);
    public abstract Task<TEntity> GetOne(Guid id);
    public abstract Task<IEnumerable<TEntity>> GetAll();
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
        }

        _disposed = true;
    }

    ~ServiceBase()
    {
        Dispose(false);
    }
}