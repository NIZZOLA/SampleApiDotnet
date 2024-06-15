using FluentResults;

namespace Store.Business.Interfaces;
public interface IServiceBase<TEntity> where TEntity : class
{
    Task<Result<TEntity>> Save(TEntity obj);
    Task<Result<TEntity>> Update(TEntity obj);
    Task<Result<bool>> Delete(Guid id);
    Task<TEntity> GetOne(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
    void Dispose();
}