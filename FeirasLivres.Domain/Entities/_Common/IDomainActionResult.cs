using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.Common;

public interface IDomainActionResult<TResult>
{
    TResult?    Value  { get; set; }
    List<Error> Errors { get; set; }
}

public static class IDomainActionResultExtensionMethods
{
    public static bool IsSuccess<T>(this IDomainActionResult<T> domainActionResult)
        => domainActionResult.Errors.None();

    public static bool HasErrors<T>(this IDomainActionResult<T> domainActionResult)
        => domainActionResult.Errors.Any();
}