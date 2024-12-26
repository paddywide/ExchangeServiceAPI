using ExchangeRate.Domain.Event;
using MediatR;


namespace ExchangeRate.Application.Contracts
{
    public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
    }
}
