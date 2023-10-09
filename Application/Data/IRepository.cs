namespace Application.Data;

public interface IRepository<TEntity, TId>
{
    Task<List<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(TId id);

    IQueryable<TEntity> GetQueryable();

    void Insert(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task SaveChangesAsync();
}