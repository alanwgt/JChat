namespace JChat.Application.Shared.CQRS;

public abstract class PaginatedWithIdQuery<T> : PaginatedQuery<T>
{
    public Guid Id { get; set; }
}