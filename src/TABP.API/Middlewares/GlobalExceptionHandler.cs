using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Constants;
using TABP.Domain.Exceptions;

namespace TABP.API.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken = default)
    {

        if(exception is FluentValidation.ValidationException validationException)
        {
            _logger.LogError(validationException, validationException.Message);
            await HandleValidationExceptionAsync(context, validationException);
            return true;
        }

        LogException(exception);

        var problemDetails = GenerateProblemDetails(context, exception);

        await problemDetails.ExecuteAsync(context);
        return true;
    }

    private void LogException(Exception exception)
    {
        if (exception is CustomException customException)
        {
            _logger.LogWarning(customException, customException.Message);
        }
        else
        {
            _logger.LogError(exception, exception.Message);
        }
    }

    private static IResult GenerateProblemDetails(
        HttpContext context,
        Exception exception)
    {
        var (statusCode, title, detail) = exception is CustomException customException
            ? MapCustomException(customException)
            : (StatusCodes.Status500InternalServerError, DefaultErrorMessages.Title, DefaultErrorMessages.Details);

        return Results.Problem(
            statusCode: statusCode,
            title: title,
            detail: detail,
            extensions: new Dictionary<string, object?>
            {
                ["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier
            }
        );
    }

    private static (int statusCode, string title, string detail)
        MapCustomException(CustomException exception)
    {
        var statusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            EntityNotFoundException => StatusCodes.Status404NotFound,
            InvalidUserCredentialsException => StatusCodes.Status401Unauthorized,
            ConfigurationException => StatusCodes.Status500InternalServerError,
            UserDuplicateException => StatusCodes.Status409Conflict,
            EmailSendingException => StatusCodes.Status500InternalServerError,
            CacheException => StatusCodes.Status503ServiceUnavailable,
            RedisCacheException => StatusCodes.Status503ServiceUnavailable,
            RedisCacheCallbackException => StatusCodes.Status500InternalServerError,
            EntityImageLimitExceededException => StatusCodes.Status400BadRequest,
            InvalidJWTConfigurationException => StatusCodes.Status500InternalServerError,
            EmptyCartException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

        return (statusCode, exception.Title, exception.Message);
    }

    private async Task HandleValidationExceptionAsync(
        HttpContext context,
        FluentValidation.ValidationException exception)
    {
        var validationErrors = exception.Errors
            .Select(err => 
                err.ToString());
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation Error",
            Detail = "One or more validation errors occurred.",
            Extensions = {["errors"] = validationErrors}
        };

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
