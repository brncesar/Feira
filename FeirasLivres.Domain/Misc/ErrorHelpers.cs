using ErrorOr;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FeirasLivres.Domain.Common
{
    public static class ErrorHelpers
    {
        public static Error GetError(ErrorType errorType, string description, string callerClass, string callerMethod)
        {
            var codeError = $"{callerClass}.{callerMethod}";
            return GetError(errorType, description, codeError);
        }

        public static Error GetError(ErrorType errorType, string description)
        {
            var callerFullName = new StackFrame(1)?.GetMethod()?.ReflectedType?.FullName ?? "";
            var methodName = Regex.Match(callerFullName, ".*<(.*)>(.*)").Groups[1].Value;
            var className = Regex.Match(callerFullName, @".*\.(.*)\+<(.*)").Groups[1].Value;

            var codeError = $"{className}.{methodName}";
            return GetError(errorType, description, codeError);
        }

        public static Error GetError(ErrorType errorType, string description, string codeError)
            => errorType switch
            {
                ErrorType.Conflict   => Error.Conflict  (codeError, description),
                ErrorType.Failure    => Error.Failure   (codeError, description),
                ErrorType.NotFound   => Error.NotFound  (codeError, description),
                ErrorType.Unexpected => Error.Unexpected(codeError, description),
                ErrorType.Validation => Error.Validation(codeError, description),
                _                    => Error.Validation(codeError, description)
            };
    }
}
