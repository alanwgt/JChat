namespace JChat.Domain.Interfaces;

public interface IAggregateRoot
{
    /// <summary>
    /// Current version of the aggregate.
    /// </summary>
    long Version { get; }

    /// <summary>
    /// Should raise a concurrency exception if the provided version is not equals to the current version of the
    /// aggregate.
    /// </summary>
    /// <param name="expectedVersion"></param>
    void ValidateVersion(long expectedVersion);

    /// <summary>
    /// Lists the history of all the events (not persisted) of the current aggregate.
    /// </summary>
    /// <returns></returns>
    IEnumerable<IDomainEvent> GetUncommittedEvents();

    /// <summary>
    /// Clears the list of uncommitted events of the aggregate.
    /// </summary>
    void ClearUncommittedEvents();

    void Load(long version, IEnumerable<IDomainEvent> history);
    void RaiseEvent(IDomainEvent @event, long originalVersion = -1);
}
