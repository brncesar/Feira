using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;
using FeirasLivres.Domain.Misc;
using FeirasLivres.Infrastructure.Data.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace FeirasLivres.Infrastructure.Data.Repository
{
    public class SubPrefeituraRepository : BaseRepository<FeirasLivresDbContext, SubPrefeitura>, ISubPrefeituraRepository
    {
        public SubPrefeituraRepository(FeirasLivresDbContext dbCtx) : base(dbCtx) { }

        public async Task<IDomainActionResult<List<FindSubPrefeituraResult>>> FindSubPrefeiturasAsync(FindSubPrefeituraParams findParams)
        {
            var domainActionResult = new DomainActionResult<List<FindSubPrefeituraResult>>();
            try
            {
                var subPrefeiturasResult = new List<FindSubPrefeituraResult>();
                var listResult = _dbSet.AsQueryable().AsNoTracking();

                if (findParams.Nome.IsNotNullOrNotEmpty())
                    listResult = listResult.Where(db => db.Nome.Contains(findParams.Nome.Trim()));

                if (findParams.Codigo.IsNotNullOrNotEmpty())
                    listResult = listResult.Where(db => db.Codigo == findParams.Codigo);

                var subPrefeiturasFound = await listResult.ToListAsync();

                subPrefeiturasFound.ForEach(subPrefeituraEntity => subPrefeiturasResult.Add(new(
                    Nome  : subPrefeituraEntity.Nome,
                    Codigo: subPrefeituraEntity.Codigo)));

                return domainActionResult.SetValue(subPrefeiturasResult);
            }
            catch (Exception ex)
            {
                return domainActionResult.ReturnRepositoryError(ex);
            }
        }

        public async Task<IDomainActionResult<SubPrefeitura>> GetByCodigoAsync(string codigo)
        {
            var domainRepositoryResult = new DomainActionResult<SubPrefeitura>();
            try
            {
                var subPrefeitura = await _dbSet.FirstOrDefaultAsync(d => d.Codigo == codigo.Trim());

                return subPrefeitura is not null
                    ? domainRepositoryResult.SetValue(subPrefeitura)
                    : domainRepositoryResult.NotFound();
            }
            catch (Exception ex)
            {
                return domainRepositoryResult.ReturnRepositoryError(ex);
            }
        }
    }
}