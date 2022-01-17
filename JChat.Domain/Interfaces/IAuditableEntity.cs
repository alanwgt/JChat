using JChat.Domain.Entities.User;

namespace JChat.Domain.Interfaces;

public interface IAuditableEntity
{
    Guid? CreatedById { get; set; }
    Guid? LastModifiedById { get; set; }
    Guid? DeletedById { get; set; }
    User? CreatedBy { get; set; }
    User? LastModifiedBy { get; set; }
    User? DeletedBy { get; set; }
}