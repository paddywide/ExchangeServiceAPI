using ExchangeRate.Domain.Event;
using ExchangeRate.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Domain.Primitive
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(Guid id)
            : base(id)
        {
        }

        protected AggregateRoot()
        {
        }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}
