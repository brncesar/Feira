using ErrorOr;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using FeirasLivres.Domain.Misc;
using FeirasLivres.Infrastructure.Data.DbCtx;

namespace FeirasLivres.Infrastructure.Data.Repository
{
    public abstract class BaseRepository<TContext, TEntity>
        where TEntity  : BaseEntity
        where TContext : DbContext
    {
        protected FeirasLivresDbContext _dbCtx { get; }
        protected DbSet<TEntity>        _dbSet { get; }

        public BaseRepository(FeirasLivresDbContext dbCtx)
        {
            _dbCtx = dbCtx;
            _dbSet = dbCtx.Set<TEntity>();
        }

        public async Task<IDomainActionResult<List<TEntity>>> GetAllAsync()
        {
            var domainActionResult = new DomainActionResult<List<TEntity>>();
            try
            {
                var listResult = await _dbSet.AsNoTracking().ToListAsync();

                return domainActionResult.SetValue(listResult);
            }
            catch (Exception ex)
            {
                return domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.Unexpected, ex.Message));
            }
        }

        public async Task<IDomainActionResult<TEntity>> GetByIdAsync(Guid id)
        {
            var domainActionResult = new DomainActionResult<TEntity>();
            try
            {
                var entity = await _dbSet.FindAsync(id);

                return entity is not null
                    ? domainActionResult.SetValue(entity)
                    : domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, typeof(TEntity).Name +" not found"));
            }
            catch (Exception ex)
            {
                return domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.Unexpected, ex.Message));
            }
        }

        public async Task<IDomainActionResult<bool>> RemoveByIdAsync(Guid id)
        {
            var domainActionResult = new DomainActionResult<bool>();
            try
            {
                var entity = await _dbSet.FindAsync(id);

                if (entity is null)
                    return domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, typeof(TEntity).Name + " not found"));

                _dbSet.Remove(entity);
                await _dbCtx.SaveChangesAsync();

                return domainActionResult.SetValue(true);
            }
            catch (Exception ex)
            {
                return domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.Unexpected, ex.Message));
            }
        }

        public async Task<IDomainActionResult<bool>> UpdateAsync(TEntity paramEntityToUpdate)
        {
            var domainActionResult = new DomainActionResult<bool>(false);

            try
            {
                var repositoryEntityToUpdate = await _dbSet.FindAsync(paramEntityToUpdate.Id);

                if (repositoryEntityToUpdate is null)
                    return domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, typeof(TEntity).Name + " not found"));

                paramEntityToUpdate.MapValuesTo(ref repositoryEntityToUpdate);
                await _dbCtx.SaveChangesAsync();

                return domainActionResult.SetValue(true);
            }
            catch (Exception ex)
            {
                return domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.Unexpected, ex.Message));
            }
        }

        public async Task<IDomainActionResult<Guid>> AddAsync(TEntity entity)
        {
            var domainActionResult = new DomainActionResult<Guid>();
            try
            {
                await _dbSet.AddAsync(entity);
                await _dbCtx.SaveChangesAsync();

                return domainActionResult.SetValue(entity.Id);
            }
            catch (Exception ex)
            {
                return domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.Unexpected, ex.Message));
            }
        }
    }
}