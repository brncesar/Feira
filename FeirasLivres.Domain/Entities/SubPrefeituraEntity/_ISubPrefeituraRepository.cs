using FeirasLivres.Domain.Entities.Common;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity
{
    public interface ISubPrefeituraRepository :
        IBaseEntityRepositoryGetAll <SubPrefeitura>,
        IBaseEntityRepositoryGetById<SubPrefeitura>
    {
        Task<IDomainActionResult<SubPrefeitura>> GetByCodigoAsync(string codigo);
    }
}
