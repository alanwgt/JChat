using JChat.Domain.Entities.Message;
using JChat.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace JChat.Infrastructure.Persistence;

public class ApplicationDbContextSeeder
{
    private readonly ModelBuilder _modelBuilder;

    public ApplicationDbContextSeeder(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        SeedReactions();
        SeedMessagePriorities();
        SeedMessageTypes();
    }

    private void SeedReactions()
    {
        var entity = _modelBuilder.Entity<Reaction>();

        entity.HasData(new Reaction("reaction.like", "thumbs-up", "1A85BA"));
        entity.HasData(new Reaction("reaction.celebrate", "celebrate", "6EAD51"));
        entity.HasData(new Reaction("reaction.love", "love", "DA7150"));
        entity.HasData(new Reaction("reaction.insightful", "insightful", "F0B85F"));
        entity.HasData(new Reaction("reaction.curious", "curious", "DCB9DA"));
        entity.HasData(new Reaction("reaction.rocket", "rocket", "CE5044"));
        entity.HasData(new Reaction("reaction.eyes", "eyes", "FFFFFF"));
    }

    private void SeedMessageTypes()
    {
        var entity = _modelBuilder.Entity<MessageBodyType>();

        entity.HasData(new MessageBodyType("message.type.audio", MessageBody.Audio));
        entity.HasData(new MessageBodyType("message.type.gif", MessageBody.Gif));
        entity.HasData(new MessageBodyType("message.type.image", MessageBody.Image));
        entity.HasData(new MessageBodyType("message.type.text", MessageBody.Text));
        entity.HasData(new MessageBodyType("message.type.video", MessageBody.Video));
    }

    private void SeedMessagePriorities()
    {
        var entity = _modelBuilder.Entity<MessagePriority>();

        entity.HasData(new MessagePriority("message.priority.normal", MessagePriorityType.Normal));
        entity.HasData(new MessagePriority("message.priority.snooze", MessagePriorityType.Snooze));
        entity.HasData(new MessagePriority("message.priority.requires_confirmation",
            MessagePriorityType.RequiresConfirmation));
        entity.HasData(new MessagePriority("message.priority.requires_confirmation_snooze",
            MessagePriorityType.RequiresConfirmationAndSnooze));
    }
}