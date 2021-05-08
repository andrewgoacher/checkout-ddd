using System;

namespace Kata.Domain.Core
{
    public abstract class Entity<TId>
    {
        protected Entity()
        {
        }

        public TId Id { get; protected set; }

        protected void Apply(object @event)
        {
            OnApply(@event);
        }

        protected abstract void OnApply(object @event);
    }
}