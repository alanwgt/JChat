using System.Text.Json;
using JChat.Domain.Entities.User;
using JChat.Domain.Interfaces;
using JChat.Infrastructure.Identity.Models;
using Ory.Kratos.Client.Model;

namespace JChat.Infrastructure.Extensions;

public static class KratosIdentityExtensions
{
    public static IUser ToApplicationUser(this KratosIdentity identity)
    {
        var traits = JsonSerializer.Deserialize<Traits>(identity.Traits.ToString());

        return new User(Guid.Parse(identity.Id), traits.Username, traits.Name.First, traits.Name.Last);
    }
}
