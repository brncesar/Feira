using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity
{
    public interface ISubPrefeituraRepository :
        IBaseEntityRepositoryGetAll <SubPrefeitura>,
        IBaseEntityRepositoryGetById<SubPrefeitura>
    {
        Task<IDomainActionResult<SubPrefeitura>> GetByCodigoAsync(string codigo);

        Task<IDomainActionResult<List<FindSubPrefeituraResult>>> FindSubPrefeiturasAsync(FindSubPrefeituraParams findParams);
    }
}
