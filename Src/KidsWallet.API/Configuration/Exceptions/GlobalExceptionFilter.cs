using FluentValidation.Results;

using KidsWallet.API.Configuration.Errors;
using KidsWallet.Shared.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using FluentValidationValidationException = FluentValidation.ValidationException;
using DataAnnotationValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace KidsWallet.API.Configuration.Exceptions;

public sealed class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : ExceptionFilterAttribute
{
    private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionHandlers = new()
    {
        { typeof(NotFoundException), HandleNotFoundException },
        { typeof(ConflictException), HandleConflictException },
        { typeof(UnauthorizedException), HandleUnauthorizedAccessException },
        { typeof(ForbiddenException), HandleForbiddenAccessException },
        { typeof(FluentValidationValidationException), HandleInvalidModelStateException },
        { typeof(DataAnnotationValidationException), HandleInvalidModelStateException }
    };
    
    public override void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Exception occurred: {Message}", context.Exception.Message);
        Exception exception = context.Exception;
        Type type = exception.GetType();
        
        if (_exceptionHandlers.TryGetValue(type, out Action<ExceptionContext>? handler))
        {
            handler.Invoke(context);
        }
        else
        {
            HandleUnknownException(context);
        }
    }
    
    private static void HandleInvalidModelStateException(ExceptionContext exceptionContext)
    {
        ModelStateDictionary modelState = new();
        
        switch (exceptionContext.Exception)
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
        
        HandleException(exceptionContext, StatusCodes.Status422UnprocessableEntity, modelState);
    }
    
    private static void HandleNotFoundException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext, StatusCodes.Status404NotFound);
    }
    
    private static void HandleConflictException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext, StatusCodes.Status409Conflict);
    }
    
    private static void HandleUnauthorizedAccessException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext, StatusCodes.Status401Unauthorized);
    }
    
    private static void HandleForbiddenAccessException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext, StatusCodes.Status403Forbidden);
    }
    
    private static void HandleUnknownException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext, StatusCodes.Status500InternalServerError);
    }
    
    private static void HandleException(ExceptionContext exceptionContext, int statusCode,
        ModelStateDictionary? modelState = null)
    {
        ApiProblemDetails details =
            StaticProblemDetailsSelector.Select(statusCode, exceptionContext.Exception.Message, modelState);
        
        exceptionContext.Result = new ObjectResult(details)
        {
            StatusCode = statusCode
        };
        
        exceptionContext.ExceptionHandled = true;
    }
}