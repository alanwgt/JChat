using System.Collections.Concurrent;
using JChat.Domain.Interfaces;

namespace JChat.Application.Notifications;

public static class OnlineUserManager
{
    private static readonly IDictionary<Guid, Tuple<string, IUser, IList<string>>> UserMap =
        new ConcurrentDictionary<Guid, Tuple<string, IUser, IList<string>>>();

    public static void AddUser(Guid userId, string connectionId, IUser user)
        => UserMap.Add(userId, new Tuple<string, IUser, IList<string>>(connectionId, user, new List<string>()));

    public static bool HasUser(Guid userId)
        => UserMap.ContainsKey(userId);

    public static Tuple<string, IUser, IList<string>> GetData(Guid userId)
        => UserMap[userId];

    public static bool RemoveUser(Guid userId)
        => UserMap.Remove(userId);

    public static void AddGroupToUser(Guid userId, string group)
        => UserMap[userId].Item3.Add(@group);

    public static bool RemoveGroupFromUser(Guid userId, string group)
        => UserMap[userId].Item3.Remove(group);

    public static string? GetConnectionString(Guid userId)
        => UserMap[userId]?.Item1 ?? null;
}