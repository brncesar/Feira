using ErrorOr;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Infrastructure.Data.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace FeirasLivres.Infrastructure.Data.Repository
{
    public class SubPrefeituraRepository : BaseRepository<FeirasLivresDbContext, SubPrefeitura>, ISubPrefeituraRepository
    {
        public SubPrefeituraRepository(FeirasLivresDbContext dbCtx) : base(dbCtx) { }

        public async Task<IDomainActionResult<SubPrefeitura>> GetByCodigoAsync(string codigo)
        {
            var subPrefeitura = await _dbSet.FirstOrDefaultAsync(d => d.Codigo == codigo.Trim());

            var domainRepositoryResult = new DomainActionResult<SubPrefeitura>(subPrefeitura);

            return subPrefeitura is not null
                ? domainRepositoryResult
                : domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Sub-prefeitura não encontrada"));
        }
    }
}