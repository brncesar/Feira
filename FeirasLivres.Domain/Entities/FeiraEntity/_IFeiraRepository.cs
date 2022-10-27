using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.FeiraEntity.Common;
using FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

namespace FeirasLivres.Domain.Entities.FeiraEntity
{
    public interface IFeiraRepository : IBaseEntityRepositoryAdd<Feira>
    {
        Task<IDomainActionResult<Feira>> GetByNumeroRegistroAsync(string numeroRegistro);

        Task<IDomainActionResult<bool>> RemoveByNumeroRegistroAsync(string numeroRegistro);

        Task<IDomainActionResult<bool>> UpdateByNumeroRegistroAsync(Feira feira);

        Task<IDomainActionResult<List<FeiraResult>>> FindFeirasAsync(FindFeiraParams findParams);
    }
}
