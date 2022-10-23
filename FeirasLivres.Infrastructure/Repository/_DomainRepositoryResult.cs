using ErrorOr;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;

namespace FeirasLivres.Infrastructure.Repository
{
    public class DomainRepositoryResult<TResult> : IDomainActionResult<TResult>
    {
        public TResult?     Value  { get; set; }
        public List<Error> Errors { get; set; } = new();

        public DomainRepositoryResult(TResult? value) => Value = value;

        public DomainRepositoryResult(List<Error> errors, TResult? value = default) =>
            (Errors, Value) = (errors, value);

        public static implicit operator DomainRepositoryResult<TResult>(TResult value)
        {
            return new DomainRepositoryResult<TResult>(value);
        }
    }

    public static class DomainRepositoryResultExtensionMethods
    {
        public static bool HasErrors<TResult>(this DomainRepositoryResult<TResult> repositoryResult) =>  repositoryResult.Errors.Any();
        public static bool IsSuccess<TResult>(this DomainRepositoryResult<TResult> repositoryResult) => !repositoryResult.Errors.Any();

        public static DomainRepositoryResult<TResult> AddError<TResult>(
            this DomainRepositoryResult<TResult> repositoryResult,
            ErrorType type,
            string description)
        {
            repositoryResult.Errors.Add(ErrorHelpers.GetError(type, description));
            return repositoryResult;
        }

        public static DomainRepositoryResult<TResult> AddErrors<TResult>(
            this DomainRepositoryResult<TResult> repositoryResult,
            List<Error> errors)
        {
            repositoryResult.Errors.AddRange(errors);
            return repositoryResult;
        }

        public static DomainRepositoryResult<TResult> SetValue<TResult>(
            this DomainRepositoryResult<TResult> repositoryResult,
            TResult newValue)
        {
            repositoryResult.Value = newValue;
            return repositoryResult;
        }
    }
}
