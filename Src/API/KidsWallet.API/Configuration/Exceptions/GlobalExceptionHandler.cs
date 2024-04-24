using FluentValidation.Results;

using KidsWallet.Shared.Exceptions;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using FluentValidationValidationException = FluentValidation.ValidationException;
using DataAnnotationValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace KidsWallet.API.Configuration.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly Dictionary<Type, Func<Exception, HttpContext, CancellationToken, Task>> _exceptionHandlers = new()
    {
        { typeof(NotFoundException), HandleNotFoundException },
        { typeof(ConflictException), HandleConflictException },
        { typeof(UnauthorizedException), HandleUnauthorizedAccessException },
        { typeof(ForbiddenException), HandleForbiddenAccessException },
        { typeof(FluentValidationValidationException), HandleInvalidModelStateException },
        { typeof(DataAnnotationValidationException), HandleInvalidModelStateException }
    };
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
        Type type = exception.GetType();
        
        if (_exceptionHandlers.TryGetValue(type, out Func<Exception, HttpContext, CancellationToken, Task>? handler))
        {
            await handler.Invoke(exception, httpContext, cancellationToken);
            
            return true;
        }
        
        await HandleUnknownException(httpContext, cancellationToken);
        
        return true;
    }
    
    private static Task HandleInvalidModelStateException(Exception exception, HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        ModelStateDictionary modelState = new();
        
        switch (exception)
        {
            case FluentValidationValidationException validationException:
                {
                    foreach (ValidationFailure error in validationException.Errors)
                    {
                        modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    
                    break;
                }
            case DataAnnotationValidationException dataAnnotationValidationException:
                {
                    foreach (string memberName in dataAnnotationValidationException.ValidationResult.MemberNames)
                    {
                        modelState.AddModelError(memberName,
                            dataAnnotationValidationException.ValidationResult.ErrorMessage ?? string.Empty);
                    }
                    
                    break;
                }
        }
        
        return HandleException(StatusCodes.Status422UnprocessableEntity, httpContext, cancellationToken,
            modelState: modelState);
    }
    
    private static Task HandleNotFoundException(Exception exception, HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        return HandleException(StatusCodes.Status404NotFound, httpContext, cancellationToken, exception);
    }
    
    private static Task HandleConflictException(Exception exception, HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        return HandleException(StatusCodes.Status409Conflict, httpContext, cancellationToken, exception);
    }
    
    private static Task HandleUnauthorizedAccessException(Exception exception, HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        return HandleException(StatusCodes.Status401Unauthorized, httpContext, cancellationToken);
    }
    
    private static Task HandleForbiddenAccessException(Exception exception, HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        return HandleException(StatusCodes.Status403Forbidden, httpContext, cancellationToken);
    }
    
    private static Task HandleUnknownException(HttpContext httpContext, CancellationToken cancellationToken)
    {
        return HandleException(StatusCodes.Status500InternalServerError, httpContext, cancellationToken);
    }
    
    private static Task HandleException(int statusCode, HttpContext httpContext, CancellationToken cancellationToken,
        Exception? exception = null, ModelStateDictionary? modelState = null)
    {
        httpContext.Response.StatusCode = statusCode;
        
        return httpContext.Response.WriteAsJsonAsync(
            StaticProblemDetailsSelector.Select(statusCode, exception?.Message, modelState), cancellationToken);
    }
}