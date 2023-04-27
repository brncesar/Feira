namespace FeirasLivres.Domain.Entities.Common;

public interface IBaseEntityRepositoryGetById<TEntity>
{
    Task<IDomainActionResult<TEntity>> GetByIdAsync(Guid id);
}

public interface IBaseEntityRepositoryAdd<TEntity>
{
    Task<IDomainActionResult<Guid>> AddAsync(TEntity entity);
}

public interface IBaseEntityRepositoryUpdate<TEntity>
{
    Task<IDomainActionResult<bool>> UpdateAsync(TEntity entity);
}

public interface IBaseEntityRepositoryDelete<TEntity>
{
    Task<IDomainActionResult<bool>> DeleteAsync(Guid id);
}

public interface IBaseEntityRepositoryGetAll<TEntity>
{
    Task<IDomainActionResult<List<TEntity>>> GetAllAsync();
}

public interface IBaseEntityRepositoryAllCrudOperations<TEntity> :
    IBaseEntityRepositoryGetById<TEntity>,
    IBaseEntityRepositoryAdd<TEntity>,
    IBaseEntityRepositoryUpdate<TEntity>,
    IBaseEntityRepositoryDelete<TEntity>,
    IBaseEntityRepositoryGetAll<TEntity>
{
}
