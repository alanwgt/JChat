using System.Collections.Concurrent;
using JChat.Domain.Interfaces;

namespace JChat.Application.Notifications;

public static class OnlineUserManager
{
    private static readonly IDictionary<Guid, Tuple<string, IUser, IList<string>>> UserMap =
        new ConcurrentDictionary<Guid, Tuple<string, IUser, IList<string>>>();

    private static readonly IDictionary<string, ISet<Guid>> GroupMembers =
        new ConcurrentDictionary<string, ISet<Guid>>();

    public static void AddUser(Guid userId, string connectionId, IUser user)
        => UserMap.Add(userId, new Tuple<string, IUser, IList<string>>(connectionId, user, new List<string>()));

    public static bool HasUser(Guid userId)
        => UserMap.ContainsKey(userId);

    public static Tuple<string, IUser, IList<string>> GetData(Guid userId)
        => UserMap[userId];

    public static bool RemoveUser(Guid userId)
    {
        if (UserMap.ContainsKey(userId))
        {
            var (_, _, groups) = UserMap[userId];

            foreach (var g in groups.ToList()) // needs the ToList method do copy the values of the list that is being modified
                RemoveGroupFromUser(userId, g);
        }

        return UserMap.Remove(userId);
    }

    public static void AddGroupToUser(Guid userId, string group)
    {
        UserMap[userId].Item3.Add(@group);

        if (!GroupMembers.ContainsKey(group))
            GroupMembers.Add(group, new HashSet<Guid>());

        GroupMembers[group].Add(userId);
    }

    public static bool RemoveGroupFromUser(Guid userId, string group)
    {
        if (GroupMembers.ContainsKey(group))
            GroupMembers[group].Remove(userId);

        return UserMap[userId].Item3.Remove(group);
    }

    public static string? GetConnectionString(Guid userId)
        => UserMap[userId]?.Item1 ?? null;

    public static IEnumerable<Guid> GetOnlineUsers(string group)
        => GroupMembers.ContainsKey(group) ? GroupMembers[group] : new List<Guid>();
}