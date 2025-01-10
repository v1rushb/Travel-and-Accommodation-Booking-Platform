using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
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
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return (statusCode, exception.Title, exception.Message);
    }
}
