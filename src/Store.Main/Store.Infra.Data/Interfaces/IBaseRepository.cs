namespace Store.Infra.Data.Interfaces;
public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();

    Task<T> GetOne(Guid id);

    Task<bool> Create(T model);

    Task<bool> Update(T model);

    Task<bool> Delete(Guid id);
}