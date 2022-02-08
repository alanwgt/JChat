using JChat.Application.Shared.Constants;
using JChat.Domain.Entities.Message;
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

        entity.HasData(new Reaction(ReactionId.Like, "reaction.like", "thumbs-up", "1A85BA"));
        entity.HasData(new Reaction(ReactionId.Celebrate, "reaction.celebrate", "celebrate", "6EAD51"));
        entity.HasData(new Reaction(ReactionId.Love, "reaction.love", "love", "DA7150"));
        entity.HasData(new Reaction(ReactionId.Insightful, "reaction.insightful", "insightful", "F0B85F"));
        entity.HasData(new Reaction(ReactionId.Curious, "reaction.curious", "curious", "DCB9DA"));
        entity.HasData(new Reaction(ReactionId.Rocket, "reaction.rocket", "rocket", "CE5044"));
        entity.HasData(new Reaction(ReactionId.Eyes, "reaction.eyes", "eyes", "FFFFFF"));
    }

    private void SeedMessageTypes()
    {
        var entity = _modelBuilder.Entity<MessageBodyType>();

        entity.HasData(new MessageBodyType(MessageBodyTypeId.Audio, "message.type.audio"));
        entity.HasData(new MessageBodyType(MessageBodyTypeId.Gif, "message.type.gif"));
        entity.HasData(new MessageBodyType(MessageBodyTypeId.Image, "message.type.image"));
        entity.HasData(new MessageBodyType(MessageBodyTypeId.Text, "message.type.text"));
        entity.HasData(new MessageBodyType(MessageBodyTypeId.Video, "message.type.video"));
        entity.HasData(new MessageBodyType(MessageBodyTypeId.ChannelEvent, "message.type.channel_event"));
    }

    private void SeedMessagePriorities()
    {
        var entity = _modelBuilder.Entity<MessagePriority>();

        entity.HasData(new MessagePriority(MessagePriorityId.Normal, "message.priority.normal"));
        entity.HasData(new MessagePriority(MessagePriorityId.Snooze, "message.priority.snooze"));
        entity.HasData(new MessagePriority(MessagePriorityId.RequiresConfirmation,
            "message.priority.requires_confirmation"));
        entity.HasData(new MessagePriority(MessagePriorityId.RequiresConfirmationAndSnooze,
            "message.priority.requires_confirmation_snooze"));
    }
}