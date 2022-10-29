using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.Common;
using FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;
using FeirasLivres.Domain.Misc;
using FeirasLivres.Infrastructure.Data.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace FeirasLivres.Infrastructure.Data.Repository;

public class FeiraRepository : BaseRepository<FeirasLivresDbContext, Feira>, IFeiraRepository
{
    public FeiraRepository(FeirasLivresDbContext dbCtx) : base(dbCtx) {}

    private async Task<Feira?> GetFeiraByNumeroRegistroAsync(string numeroRegistro)
        => await _dbSet.SingleOrDefaultAsync(x => x.NumeroRegistro == numeroRegistro.Trim());

    public async Task<IDomainActionResult<Feira>> GetByNumeroRegistroAsync(string numeroRegistro)
    {
        var domainActionResult = new DomainActionResult<Feira>();
        try
        {
            var feira = await GetFeiraByNumeroRegistroAsync(numeroRegistro);

            return feira is not null
                ? domainActionResult.SetValue(feira)
                : domainActionResult.NotFound();
        }
        catch (Exception ex)
        {
            return domainActionResult.ReturnRepositoryError(ex);
        }
    }

    public async Task<IDomainActionResult<bool>> RemoveByNumeroRegistroAsync(string numeroRegistro)
    {
        var domainActionResult = new DomainActionResult<bool>();
        try
        {
            var feira = await GetFeiraByNumeroRegistroAsync(numeroRegistro);

            if (feira is null) return domainActionResult.NotFound();

            _dbSet.Remove(feira);
            await _dbCtx.SaveChangesAsync();

            return domainActionResult.SetValue(true);
        }
        catch (Exception ex)
        {
            return domainActionResult.ReturnRepositoryError(ex);
        }
    }

    public async Task<IDomainActionResult<bool>> UpdateByNumeroRegistroAsync(Feira feira)
    {
        var domainActionResult = new DomainActionResult<bool>(false);

        try
        {
            await UpdateAsync(feira);

            return domainActionResult.SetValue(true);
        }
        catch (Exception ex)
        {
            return domainActionResult.ReturnRepositoryError(ex);
        }
    }

    public async Task<IDomainActionResult<List<FeiraResult>>> FindFeirasAsync(FindFeiraParams findParams)
    {
        var domainRepositoryResult = new DomainActionResult<List<FeiraResult>>();
        try
        {
            var feirasResult = new List<FeiraResult>();
            var listResult = _dbSet.AsQueryable().AsNoTracking();

            if (findParams.Nome.IsNotNullOrNotEmpty())
                listResult = listResult.Where(db => db.Nome.Contains(findParams.Nome.Trim()));

            if (findParams.Bairro.IsNotNullOrNotEmpty())
                listResult = listResult.Where(db => db.EnderecoBairro.Contains(findParams.Bairro.Trim()));

            if (findParams.CodDistrito.IsNotNullOrNotEmpty())
                listResult = listResult.Where(db => db.Distrito.Codigo == findParams.CodDistrito);

            if (findParams.Regiao5 is not null)
            {
                var isRegiao5Parsed = Enum.TryParse(findParams.Regiao5, true, out Regiao5 enumRegiao5FromString);

                if(isRegiao5Parsed)
                    listResult = listResult.Where(db => db.Regiao5 == enumRegiao5FromString);
                else
                {
                    var validRegions5 = Enum.GetValues(typeof(Regiao5)).Cast<Regiao5>();
                    return domainRepositoryResult.AddError(ErrorHelpers.GetError(
                        ErrorType.Validation,
                        $"Regiao5 invalid: {findParams.Regiao5}. Possible values are: {string.Join(", ", validRegions5)}",
                        "Feira.Regiao5"));
                }
            }

            var feirasFound = await listResult
                .Include(f => f.Distrito)
                .Include(f => f.SubPrefeitura)
                .ToListAsync();

            feirasFound.ForEach(feiraEntity => feirasResult.Add(feiraEntity.ToFeiraResult()));

            return domainRepositoryResult.SetValue(feirasResult);
        }
        catch (Exception ex)
        {
            return domainRepositoryResult.ReturnRepositoryError(ex);
        }
    }
}
