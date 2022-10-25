using ErrorOr;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using FeirasLivres.Domain.Misc;
using FeirasLivres.Infrastructure.Data.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace FeirasLivres.Infrastructure.Data.Repository
{
    public class DistritoRepository : BaseRepository<FeirasLivresDbContext, Distrito>, IDistritoRepository
    {
        public DistritoRepository(FeirasLivresDbContext dbCtx) : base(dbCtx) { }

        public async Task<IDomainActionResult<List<FindDistritoResult>>> FindDistritosAsync(FindDistritoParams findParams)
        {
            var domainActionResult = new DomainActionResult<List<FindDistritoResult>>();
            try
            {
                var distritosResult = new List<FindDistritoResult>();
                var listResult = _dbSet.AsQueryable().AsNoTracking();

                if (findParams.Nome.IsNotNullOrNotEmpty())
                    listResult = listResult.Where(db => db.Nome.Contains(findParams.Nome.Trim()));

                if (findParams.Codigo.IsNotNullOrNotEmpty())
                    listResult = listResult.Where(db => db.Codigo == findParams.Codigo);

                var distritosFound = await listResult.ToListAsync();

                distritosFound.ForEach(distritoEntity => distritosResult.Add(new(
                    Nome   : distritoEntity.Nome,
                    Codigo : distritoEntity.Codigo)));

                return domainActionResult.SetValue(distritosResult);
            }
            catch (Exception ex)
            {
                return domainActionResult.AddError(ErrorHelpers.GetError(ErrorType.Unexpected, ex.Message));
            }
        }

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