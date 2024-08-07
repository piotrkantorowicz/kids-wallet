﻿using FluentValidation.Results;

using KidsWallet.API.Configuration.Errors;
using KidsWallet.Shared.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using FluentValidationValidationException = FluentValidation.ValidationException;
using DataAnnotationValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace KidsWallet.API.Configuration.Exceptions;

public sealed class GlobalExceptionFilter : ExceptionFilterAttribute
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

    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        _logger.LogError(exception: context.Exception, message: "Exception occurred: {Message}",
            context.Exception.Message);

        Exception exception = context.Exception;
        Type type = exception.GetType();

        if (_exceptionHandlers.TryGetValue(key: type, value: out Action<ExceptionContext>? handler))
        {
            handler.Invoke(obj: context);
        }
        else
        {
            HandleUnknownException(exceptionContext: context);
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
                        modelState.AddModelError(key: error.PropertyName, errorMessage: error.ErrorMessage);
                    }

                    break;
                }
            case DataAnnotationValidationException dataAnnotationValidationException:
                {
                    foreach (string memberName in dataAnnotationValidationException.ValidationResult.MemberNames)
                    {
                        modelState.AddModelError(key: memberName,
                            errorMessage: dataAnnotationValidationException.ValidationResult.ErrorMessage ??
                                          string.Empty);
                    }

                    break;
                }
        }

        HandleException(exceptionContext: exceptionContext, statusCode: StatusCodes.Status422UnprocessableEntity,
            modelState: modelState);
    }

    private static void HandleNotFoundException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext: exceptionContext, statusCode: StatusCodes.Status404NotFound);
    }

    private static void HandleConflictException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext: exceptionContext, statusCode: StatusCodes.Status409Conflict);
    }

    private static void HandleUnauthorizedAccessException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext: exceptionContext, statusCode: StatusCodes.Status401Unauthorized);
    }

    private static void HandleForbiddenAccessException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext: exceptionContext, statusCode: StatusCodes.Status403Forbidden);
    }

    private static void HandleUnknownException(ExceptionContext exceptionContext)
    {
        HandleException(exceptionContext: exceptionContext, statusCode: StatusCodes.Status500InternalServerError);
    }

    private static void HandleException(ExceptionContext exceptionContext, int statusCode,
        ModelStateDictionary? modelState = null)
    {
        ApiProblemDetails details = StaticProblemDetailsSelector.Select(statusCode: statusCode,
            detail: exceptionContext.Exception.Message, modelState: modelState);

        exceptionContext.Result = new ObjectResult(value: details)
        {
            StatusCode = statusCode
        };

        exceptionContext.ExceptionHandled = true;
    }
}