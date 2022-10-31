using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Serilog;
using System.Diagnostics;

namespace FeirasLivres.Api.Misc;

internal sealed class CustomProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public CustomProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
        => _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int?        statusCode = null,
        string?     title      = null,
        string?     type       = null,
        string?     detail     = null,
        string?     instance   = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status   = statusCode,
            Title    = title,
            Type     = type,
            Detail   = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext          httpContext,
        ModelStateDictionary modelStateDictionary,
        int?                 statusCode = null,
        string?              title      = null,
        string?              type       = null,
        string?              detail     = null,
        string?              instance   = null)
    {
        if (modelStateDictionary == null)
            throw new ArgumentNullException(nameof(modelStateDictionary));

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status   = statusCode,
            Type     = type,
            Detail   = detail,
            Instance = instance,
        };

        if (title != null)
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type  ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
            problemDetails.Extensions["traceId"] = traceId;


        LogErrors(httpContext, problemDetails);
    }

    private void LogErrors(HttpContext httpContext, ProblemDetails problemDetails) {
        var applicationErrors = httpContext?.Items[HttpContextApplicationErrorsKey] as List<Error>;

        if (applicationErrors is null || applicationErrors.None()) return;

        var firstError = applicationErrors.First();
        Log.Error($"{firstError.Type} » {firstError.Code} - {firstError.Description}");

        foreach (var error in applicationErrors.Skip(1)) {
            Log.Error($"{error.Type} » {error.Code} - {error.Description}");
            problemDetails.Extensions.Add("errorCodes", $"{error.Code} » {error.Description} ({error.Type})");
        }
    }

    public const string HttpContextApplicationErrorsKey = "Errors";
}
