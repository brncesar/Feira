using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;

namespace FeirasLivres.Domain.Entities.FeiraEntity
{
    public interface IFeiraRepository : IBaseEntityRepositoryAdd<Feira>
    {
        Task<IDomainActionResult<Feira>> GetByNumeroRegistroAsync(string numeroRegistro);

        Task<IDomainActionResult<bool>> RemoveByNumeroRegistroAsync(string numeroRegistro);

        Task<IDomainActionResult<bool>> UpdateByNumeroRegistroAsync(EditExistingFeiraParams feira);
    }
}
