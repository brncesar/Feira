namespace FeirasLivres.Domain.Entities.Common;

public interface IUseCase<TParams, TResult>
{
    Task<IDomainActionResult<TResult>> Execute(TParams parameters);
}
