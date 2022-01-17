using JChat.Domain.SeedWork;
using MediatR;

namespace JChat.Application.Shared.Models;

public record DomainEventNotification<TDomainEvent>(TDomainEvent DomainEvent) : INotification
    where TDomainEvent : DomainEvent;
