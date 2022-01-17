using System.ComponentModel.DataAnnotations.Schema;
using JChat.Domain.Entities.User;
using JChat.Domain.Interfaces;

namespace JChat.Domain.SeedWork;

public abstract class AuditableEntity : Entity, IAuditableEntity
{
    public Guid? CreatedById { get; set; }
    public Guid? LastModifiedById { get; set; }
    public Guid? DeletedById { get; set; }

    [ForeignKey("CreatedById")] public User? CreatedBy { get; set; }
    [ForeignKey("LastModifiedById")] public User? LastModifiedBy { get; set; }
    [ForeignKey("DeletedById")] public User? DeletedBy { get; set; }
}