using ErrorOr;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FeirasLivres.Domain.Common
{
    public static class ErrorHelpers
    {
        public static Error GetError(ErrorType errorType, string description, [CallerMemberName] string callerName = "")
        {
            var callerClass = new StackFrame(1)?.GetMethod()?.ReflectedType?.Name ?? "";
            var codeError = $"{callerClass}.{callerName}";

            return errorType switch
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
}
