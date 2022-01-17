namespace JChat.Domain.Enums;

public enum MessagePriorityType
{
    RequiresConfirmationAndSnooze = (char) 128,
    RequiresConfirmation = (char) 100,
    Snooze = (char) 50,
    Normal = (char) 0,
}
