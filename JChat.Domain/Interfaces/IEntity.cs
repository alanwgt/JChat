namespace JChat.Domain.Interfaces;

public interface IEntity<out T>
{
    public T Id { get; }
}
