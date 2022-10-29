using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace FeirasLivres.Domain.Common;


// This class Error is copy of Error from ErrorOr project (https://github.com/amantinband/error-or)
public readonly struct Error : IEquatable<Error>
{
    //
    // Summary:
    //     Gets the unique error code.
    public string Code { get; }

    //
    // Summary:
    //     Gets the error description.
    public string Description { get; }

    //
    // Summary:
    //     Gets the error type.
    public ErrorType Type { get; }

    //
    // Summary:
    //     Gets the numeric value of the type.
    public int NumericType { get; }

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
        NumericType = (int)type;
    }

    //
    // Summary:
    //     Creates an ErrorOr.Error of type ErrorOr.ErrorType.Failure from a code and description.
    //
    // Parameters:
    //   code:
    //     The unique error code.
    //
    //   description:
    //     The error description.
    public static Error Failure(string code = "General.Failure", string description = "A failure has occurred.")
    {
        return new Error(code, description, ErrorType.Failure);
    }

    //
    // Summary:
    //     Creates an ErrorOr.Error of type ErrorOr.ErrorType.Unexpected from a code and
    //     description.
    //
    // Parameters:
    //   code:
    //     The unique error code.
    //
    //   description:
    //     The error description.
    public static Error Unexpected(string code = "General.Unexpected", string description = "An unexpected error has occurred.")
    {
        return new Error(code, description, ErrorType.Unexpected);
    }

    //
    // Summary:
    //     Creates an ErrorOr.Error of type ErrorOr.ErrorType.Validation from a code and
    //     description.
    //
    // Parameters:
    //   code:
    //     The unique error code.
    //
    //   description:
    //     The error description.
    public static Error Validation(string code = "General.Validation", string description = "A validation error has occurred.")
    {
        return new Error(code, description, ErrorType.Validation);
    }

    //
    // Summary:
    //     Creates an ErrorOr.Error of type ErrorOr.ErrorType.Conflict from a code and description.
    //
    // Parameters:
    //   code:
    //     The unique error code.
    //
    //   description:
    //     The error description.
    public static Error Conflict(string code = "General.Conflict", string description = "A conflict error has occurred.")
    {
        return new Error(code, description, ErrorType.Conflict);
    }

    //
    // Summary:
    //     Creates an ErrorOr.Error of type ErrorOr.ErrorType.NotFound from a code and description.
    //
    // Parameters:
    //   code:
    //     The unique error code.
    //
    //   description:
    //     The error description.
    public static Error NotFound(string code = "General.NotFound", string description = "A 'Not Found' error has occurred.")
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    //
    // Summary:
    //     Creates an ErrorOr.Error with the given numeric type, code, and description.
    //
    // Parameters:
    //   type:
    //     An integer value which represents the type of error that occurred.
    //
    //   code:
    //     The unique error code.
    //
    //   description:
    //     The error description.
    public static Error Custom(int type, string code, string description)
    {
        return new Error(code, description, (ErrorType)type);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Error");
        stringBuilder.Append(" { ");
        if (PrintMembers(stringBuilder))
        {
            stringBuilder.Append(' ');
        }

        stringBuilder.Append('}');
        return stringBuilder.ToString();
    }

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("Code = ");
        builder.Append((object?)Code);
        builder.Append(", Description = ");
        builder.Append((object?)Description);
        builder.Append(", Type = ");
        builder.Append(Type.ToString());
        builder.Append(", NumericType = ");
        builder.Append(NumericType.ToString());
        return true;
    }

    public static bool operator !=(Error left, Error right)
    {
        return !(left == right);
    }

    public static bool operator ==(Error left, Error right)
    {
        return left.Equals(right);
    }

    public override int GetHashCode()
    {
        return ((EqualityComparer<string>.Default.GetHashCode(Code) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description)) * -1521134295 + EqualityComparer<ErrorType>.Default.GetHashCode(Type)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(NumericType);
    }

    public override bool Equals(object obj)
    {
        if (obj is Error)
        {
            return Equals((Error)obj);
        }

        return false;
    }

    public bool Equals(Error other)
    {
        if (EqualityComparer<string>.Default.Equals(Code, other.Code) && EqualityComparer<string>.Default.Equals(Description, other.Description) && EqualityComparer<ErrorType>.Default.Equals(Type, other.Type))
        {
            return EqualityComparer<int>.Default.Equals(NumericType, other.NumericType);
        }

        return false;
    }
}

public enum ErrorType
{
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound
}

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
            ErrorType.Conflict => Error.Conflict(codeError, description),
            ErrorType.Failure => Error.Failure(codeError, description),
            ErrorType.NotFound => Error.NotFound(codeError, description),
            ErrorType.Unexpected => Error.Unexpected(codeError, description),
            ErrorType.Validation => Error.Validation(codeError, description),
            _ => Error.Validation(codeError, description)
        };
}