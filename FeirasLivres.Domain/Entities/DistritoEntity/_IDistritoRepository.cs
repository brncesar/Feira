using FeirasLivres.Domain.Entities.Common;

namespace FeirasLivres.Domain.Entities.DistritoEntity
{
    public interface IDistritoRepository :
        IBaseEntityRepositoryGetAll<Distrito>,
        IBaseEntityRepositoryGetById<Distrito>
    {
        Task<IDomainActionResult<Distrito>> GetByCodigoAsync(string codigo);
    }
}
