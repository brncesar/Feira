using FluentValidation.Results;
using System.Linq.Expressions;

namespace FeirasLivres.Domain.Misc
{
    public static class FluentValidationValidationResultExtensions
    {
        public static bool IsPropInvalid(this ValidationResult source, string propName)
        {
            return source.Errors.Any(err => err.PropertyName == propName);
        }

        public static bool IsPropInvalid<T>(this ValidationResult source, T targetObj, Expression<Func<T, object>> prop)
        {
            return source.Errors.Any(err => err.PropertyName == nameof(prop));
        }

        public static bool IsPropValid(this ValidationResult source, string propName)
        {
            return source.Errors.None(err => err.PropertyName == propName);
        }

        public static bool IsPropValid<T>(this ValidationResult source, T targetObj, Expression<Func<T, object>> prop)
        {
            return source.Errors.None(err => err.PropertyName == nameof(prop));
        }
    }
}
