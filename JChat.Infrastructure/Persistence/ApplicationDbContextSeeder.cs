using JChat.Domain.Entities.Message;

namespace JChat.Infrastructure.Persistence;

public class ApplicationDbContextSeeder
{
    public async Task SeedReactions(ApplicationDbContext context, Guid userId)
    {
        if (context.Reactions.Any())
            return;

        context.Reactions.Add(new Reaction("reaction.like", "thumbs-up", "1A85BA"));
        context.Reactions.Add(new Reaction("reaction.celebrate", "celebrate", "6EAD51"));
        context.Reactions.Add(new Reaction("reaction.love", "love", "DA7150"));
        context.Reactions.Add(new Reaction("reaction.insightful", "insightful", "F0B85F"));
        context.Reactions.Add(new Reaction("reaction.curious", "curious", "DCB9DA"));
        context.Reactions.Add(new Reaction("reaction.rocket", "rocket", "CE5044"));
        context.Reactions.Add(new Reaction("reaction.eyes", "eyes", "FFFFFF"));

        await context.SaveChangesAsync();
    }
}