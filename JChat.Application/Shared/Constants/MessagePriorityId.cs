namespace JChat.Application.Shared.Constants;

public static class MessagePriorityId
{
    public static readonly Guid Normal = Guid.Parse("195348d7-5de2-465c-ab01-89e76b7823cf");
    public static readonly Guid Snooze = Guid.Parse("8eafa8fe-dfae-4143-8abe-53e57d2d427c");
    public static readonly Guid RequiresConfirmation = Guid.Parse("55cd213f-5a4f-40a1-8b3e-1b0aef8c0847");
    public static readonly Guid RequiresConfirmationAndSnooze = Guid.Parse("2efd5971-81aa-4f7e-94ff-c49b2827f945");
}