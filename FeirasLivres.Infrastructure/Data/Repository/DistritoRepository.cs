using ErrorOr;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Infrastructure.Data.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace FeirasLivres.Infrastructure.Data.Repository
{
    public class DistritoRepository : BaseRepository<FeirasLivresDbContext, Distrito>, IDistritoRepository
    {
        public DistritoRepository(FeirasLivresDbContext dbCtx) : base(dbCtx) { }


        public async Task<IDomainActionResult<Distrito>> GetByCodigoAsync(string codigo)
        {
            var distrito = await _dbSet.FirstOrDefaultAsync(d => d.Codigo == codigo.Trim());

            var domainRepositoryResult = new DomainActionResult<Distrito>(distrito);

            return distrito is not null
                ? domainRepositoryResult
                : domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Distrito não encontratdo"));
        }
    }
}