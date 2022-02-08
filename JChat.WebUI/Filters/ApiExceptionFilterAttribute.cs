using JChat.Application.Shared.Exceptions;
using JChat.Domain.Enums;
using JChat.Domain.Exceptions;
using JChat.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ApplicationException = JChat.Application.Shared.Exceptions.ApplicationException;

namespace JChat.WebUI.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<ApiExceptionFilterAttribute> _logger;
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
    {
        _logger = logger;
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(FluentValidation.ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            { typeof(DomainValidationException), HandleDomainValidationException },
            { typeof(ApplicationException), HandleApplicationException },
            { typeof(DomainException), HandleDomainException },
            { typeof(ViolatesUniqueKeyConstraintException), HandleUniqueKeyViolationException },
            { typeof(InvalidOperationException), HandleInvalidOperationException },
            { typeof(BadRequestException), HandleBadRequestException }
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();

        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }


    private static void HandleValidationException(ExceptionContext context)
    {
        ValidationProblemDetails details;

        if (context.Exception is FluentValidation.ValidationException exc)
        {
            var errs = exc.Errors
                .ToDictionary(err => err.PropertyName,
                    err => new[]
                    {
                        err.ErrorMessage, err.PropertyName,
                        err.AttemptedValue?.ToString() ?? "None given"
                    });

            details = new ValidationProblemDetails(errs)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
        }
        else
        {
            var exception = (ValidationException)context.Exception;

            details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
        }

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private static void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;

        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }

    private static void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };

        context.ExceptionHandled = true;
    }

    private static void HandleForbiddenAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };

        context.ExceptionHandled = true;
    }

    private static void HandleDomainValidationException(ExceptionContext context)
    {
        var exception = (DomainValidationException)context.Exception;
        var errMessage = exception.ExceptionType switch
        {
            ValidationExceptionType.Empty => "cannot be empty",
            ValidationExceptionType.MaxLength => "cannot be greater than",
            ValidationExceptionType.MinLength => "cannot be less than",
            _ => "unknown",
        };
        var errors = new Dictionary<string, string[]>
        {
            { exception.Prop, new string[] { errMessage } }
        };

        var details = new ValidationProblemDetails(errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleUniqueKeyViolationException(ExceptionContext context)
    {
        var exception = (ViolatesUniqueKeyConstraintException)context.Exception;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "The request violated an unique constraint in the database",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = exception.Message
        };

        context.Result = new BadRequestObjectResult(details)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        context.ExceptionHandled = true;
    }

    private static void HandleDomainException(ExceptionContext context)
    {
        var exception = (DomainException)context.Exception;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        if (exception.Details != null)
        {
            details.Detail = exception.Details;
        }

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        context.ExceptionHandled = true;
    }

    private static void HandleBadRequestException(ExceptionContext context)
    {
        var exception = (BadRequestException)context.Exception;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        if (exception.Details != null)
        {
            details.Detail = exception.Details;
        }

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        context.ExceptionHandled = true;
    }

    private static void HandleApplicationException(ExceptionContext context)
    {
        var exception = (ApplicationException)context.Exception;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        if (exception.Details != null)
        {
            details.Detail = exception.Details;
        }

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status409Conflict
        };

        context.ExceptionHandled = true;
    }

    private static void HandleInvalidOperationException(ExceptionContext context)
    {
        var exception = (InvalidOperationException)context.Exception;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = "exceptions.resource_not_found"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "unknown exception caught");

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}