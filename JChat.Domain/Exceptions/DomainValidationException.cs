using JChat.Domain.Enums;

namespace JChat.Domain.Exceptions;

public class DomainValidationException : DomainException
{
    public readonly ValidationExceptionType ExceptionType;
    public readonly string Prop;

    public DomainValidationException(ValidationExceptionType exceptionType, string prop, object? shouldBe = null)
        : base(
            $"DomainValidationException: {prop} raised validation exception type {Enum.GetName(typeof(ValidationExceptionType), exceptionType)}")
    {
        ExceptionType = exceptionType;
        Prop = prop;
    }
}