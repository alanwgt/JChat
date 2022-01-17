namespace JChat.Domain.Interfaces;

public interface IUser
{
    public Guid Id { get; }
    public string Username { get; }
    public string Firstname { get; }
    public string Lastname { get; }
}
