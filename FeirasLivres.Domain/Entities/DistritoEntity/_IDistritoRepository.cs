using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;

namespace FeirasLivres.Domain.Entities.DistritoEntity
{
    public interface IDistritoRepository :
        IBaseEntityRepositoryGetAll<Distrito>,
        IBaseEntityRepositoryGetById<Distrito>
    {
        Task<IDomainActionResult<Distrito>> GetByCodigoAsync(string codigo);

        Task<IDomainActionResult<List<FindDistritoResult>>> FindDistritosAsync(FindDistritoParams findParams);
    }
}
