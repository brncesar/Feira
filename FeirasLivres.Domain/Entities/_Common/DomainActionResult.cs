using ErrorOr;
using FeirasLivres.Domain.Common;
using FluentValidation.Results;

namespace FeirasLivres.Domain.Entities.Common
{
    public class DomainActionResult<T> : IDomainActionResult<T>
    {
        public T? Value { get; set; }
        public List<Error> Errors { get; set; } = new();
        public bool IsSuccess { get => !Errors.Any(); }
        public bool HasErrors { get => Errors.Any(); }

        public DomainActionResult() { }

        public DomainActionResult(T? value) => Value = value;

        public DomainActionResult(List<Error> errors, T? value = default)
            => (Errors, Value) = (errors, value);

        public DomainActionResult(List<ValidationFailure> fluentValidationFailures)
            => this.AddFluentValidationErrors(fluentValidationFailures);

        public static implicit operator DomainActionResult<T>(T value)
        {
            return new DomainActionResult<T>(value);
        }
    }


    public static class DomainServiceResultExtensionMethods
    {
        public static IDomainActionResult<T> AddError<T>(
            this IDomainActionResult<T> serviceResult,
            Error error)
        {
            serviceResult.Errors.Add(error);
            return serviceResult;
        }
        public static IDomainActionResult<T> AddErrors<T>(
            this IDomainActionResult<T> serviceResult,
            List<Error> errors)
        {
            serviceResult.Errors.AddRange(errors);
            return serviceResult;
        }
        public static IDomainActionResult<T> AddErrorsFrom<T>(
            this IDomainActionResult<T> serviceResult,
            IDomainActionResult<T> anotherServiceResult)
        {
            serviceResult.Errors.AddRange(anotherServiceResult.Errors);
            return serviceResult;
        }

        public static IDomainActionResult<T> AddFluentValidationErrors<T>(
            this IDomainActionResult<T> serviceResult,
            List<ValidationFailure> fluentValidationFailures)
        {
            fluentValidationFailures.ForEach(
                fvf => serviceResult.Errors.Add(
                    ErrorHelpers.GetError(ErrorType.Validation, fvf.ErrorMessage)));

            return serviceResult;
        }

        public static IDomainActionResult<TResult> SetValue<TResult>(
            this IDomainActionResult<TResult> repositoryResult,
            TResult newValue)
        {
            repositoryResult.Value = newValue;
            return repositoryResult;
        }
    }
}